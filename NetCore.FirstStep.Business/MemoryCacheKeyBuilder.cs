using NetCore.FirstStep.Business.Commands;
using NetCore.FirstStep.Business.Queries;
using NetCore.FirstStep.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Business
{
    public class MemoryCacheKeyBuilder :
        ICacheKeyBuilder<GetAccountIntent>,
        ICacheKeyBuilder<GetTransactionIntent>,
        ICacheKeyBuilder<SubmitTransactionIntent>,
        ICacheKeyBuilder<UpdateTransactionStatusIntent>,
        ICacheKeyBuilder<ValidateTransactionIntent>,
        ICacheKeyBuilder<CreateAccountIntent>,
        ICacheKeyBuilder<CreateOperationIntent>
    {
        private string GetAccountCacheKey(string key)
        {
            return $"account({key})";
        }

        private string GetTransactionCacheKey(string key)
        {
            return $"transaction({key})";
        }

        public CacheKey GetCacheKey(GetAccountIntent intent) => new CacheKey(GetAccountCacheKey(intent.Key));
        public CacheKey GetCacheKey(GetTransactionIntent intent) => new CacheKey(GetTransactionCacheKey(intent.Key));
        public CacheKey GetCacheKey(UpdateTransactionStatusIntent intent) => new CacheKey(GetTransactionCacheKey(intent.TransactionId));

        public CacheKey GetCacheKey(CreateOperationIntent intent) => new CacheKey(
            GetAccountCacheKey(intent.Sender.Key), 
            new CacheKey(GetAccountCacheKey(intent.Recipient.Key)));

        public CacheKey GetCacheKey(ValidateTransactionIntent intent) => null;
        public CacheKey GetCacheKey(SubmitTransactionIntent intent) => null;
        public CacheKey GetCacheKey(CreateAccountIntent intent) => null;
    }
}
