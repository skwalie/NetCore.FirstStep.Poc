using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using NetCore.FirstStep.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace NetCore.FirstStep.Business.Mongo
{
    public class MongoBusinessManager : IFirstStepBusinessManager
    {
        public MongoBusinessManager(IMongoConfig config, DtoMapper mapper)
        {
            Client = new MongoClient(config.ConnectionString);
            Database = Client.GetDatabase(config.Database);
            Mapper = mapper;
        }

        public MongoBusinessManager(IMongoConfig config) : this(config, new DtoMapper())
        {

        }
        private MongoClient Client { get; }
        private IMongoDatabase Database { get; }
        public DtoMapper Mapper { get; }

        public async Task<Account> AddOperation(Operation operation)
        {
            var accounts = Database.GetCollection<AccountDto>("account");

            var projection = Builders<AccountDto>.Projection.Exclude("_id");
            var account = await GetAccount(operation.AccountKey);
            account.AddOperation(operation);

            var dto = Mapper.Map(account);

            var filterDef = new ExpressionFilterDefinition<AccountDto>(acc => acc.Key == account.Key);

            //var options = new UpdateOptions()
            //{
            //    Projection = projection,
            //};

            await accounts.ReplaceOneAsync(
                filterDef,
                dto);

            return account;
        }

        public async Task<Account> CreateAccount()
        {
            var accounts = Database.GetCollection<AccountDto>("account");
            var newAccount = new Account();
            await accounts.InsertOneAsync(Mapper.Map(newAccount));
            return newAccount;
        }

        public async Task<Transaction> CreateTransaction(string senderKey, string recipientKey, decimal sum)
        {
            var transactionCollection = Database.GetCollection<TransactionDto>("transaction");
            var newTransaction = new Transaction(senderKey, recipientKey, sum);
            await transactionCollection.InsertOneAsync(Mapper.Map(newTransaction));
            return newTransaction;
        }

        public async Task<Account> GetAccount(string accountKey)
        {
            var accounts = Database.GetCollection<AccountDto>("account");
            var projection = Builders<AccountDto>.Projection.Exclude("_id");
            var findOptions = new FindOptions<AccountDto> { Projection = projection };

            var result = await accounts.FindAsync(
                acc => acc.Key == accountKey,
                findOptions);

            var dto = await result.SingleOrDefaultAsync();

            if (dto == null) return null;
            var account = Mapper.Map(dto);

            var allIds = dto.CurrentSituation.Operations.Select(op => op.TransactionId);

            var transacProjection = Builders<TransactionDto>.Projection.Exclude("_id");

            var transacFilter = new ExpressionFilterDefinition<TransactionDto>(transac => allIds.Contains(transac.TransactionId));
            var transactionCollection = Database.GetCollection<TransactionDto>("transaction");
            var transacfindOptions = new FindOptions<TransactionDto>() { Projection = transacProjection };

            var transacCrursor = await  transactionCollection.FindAsync(
                transacFilter,
                transacfindOptions);

            var transactionsDto = transacCrursor.ToList();
            var transactions = transactionsDto.Select(t => Mapper.Map(t));

            foreach (var o in dto.CurrentSituation.Operations)
            {
                account.AddOperation(
                    Mapper.Map(
                        o, 
                        dto.Key, 
                        transactions.Single(t => t.Id == o.TransactionId)));
            }

            return account;
        }

        public async Task<Transaction> GetTransaction(string transactionId)
        {
            var mongoCollection = Database.GetCollection<TransactionDto>("transaction");
            var projection = Builders<TransactionDto>.Projection.Exclude("_id");
            var findOptions = new FindOptions<TransactionDto> { Projection = projection };

            var dto = await mongoCollection.FindAsync(
                transaction => transaction.TransactionId == transactionId,
                findOptions);

            return Mapper.Map(dto.SingleOrDefault());
        }

        public Task<IEnumerable<Transaction>> GetTransactionsFromStatus(string accountKey, Transaction.Status status)
        {
            throw new NotImplementedException();
        }

        public async Task<Transaction> UpdateTransactionStatus(string transactionId, Transaction.Status status)
        {
            var transaction = await GetTransaction(transactionId);

            if (transaction == null) return null;
            transaction.SetTransactionStatus(status);

            var transactionCollection = Database.GetCollection<TransactionDto>("transaction");
            var upateDef = Builders<TransactionDto>.Update
                .Set(x => x.TransactionStatus, transaction.TransactionStatus.ToString())
                .Set(x => x.StatusTimestamp, transaction.StatusTimestamp);

            transactionCollection.UpdateOne(
                t => t.TransactionId == transactionId,
                upateDef
                );

            return transaction;
        }
    }
} 

