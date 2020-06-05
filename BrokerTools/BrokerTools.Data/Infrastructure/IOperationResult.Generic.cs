
using System;

namespace BrokerTools.Data.Infrastructure
{
    public interface IOperationResult<T> : IOperationResult
    {
        T Result { get; }
    }
}
