using System;
using System.Collections.Generic;
using System.Text;
using BackendDotNet.Application.Common;

namespace BackendDotNet.Application.Abstractions.Messaging
{
    public interface IDispatcher
    {
        Task<Result<TResponse>> Send<TResponse>(ICommand<TResponse> command, CancellationToken ct = default);
        Task<Result<TResponse>> Query<TResponse>(IQuery<TResponse> query, CancellationToken ct = default);
    }
}
