using BackendDotNet.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackendDotNet.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken ct);
}