using System;

namespace BrokerTools.Data.Infrastructure
{
    public interface IOperationResult
    {
        Exception Error { get; }
    }
}
