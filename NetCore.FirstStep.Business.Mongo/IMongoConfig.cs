using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.FirstStep.Business.Mongo
{
    public interface IMongoConfig
    {
        string ConnectionString { get;  }
        string Database { get; }
    }
}
