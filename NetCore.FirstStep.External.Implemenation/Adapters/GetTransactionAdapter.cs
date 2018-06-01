//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NetCore.FirstStep.Business.Arguments;
//using NetCore.FirstStep.Core;
//using NetCore.FirstStep.Domain;

//namespace NetCore.FirstStep.Business.Adapters
//{
//    public class GetTransactionAdapter : IAdapter<GetTransactionArgument, Transaction>
//    {
//        public async Task<IResult<Transaction>> Adapt(GetTransactionArgument input)
//        {
//            var transaction = Stub.Transactions.SingleOrDefault(x => x.Id == input.TransactionId);
//            var result = new ResultBase<Transaction>(transaction);
//            return await Task.FromResult(result);
//        }
//    }
//}
