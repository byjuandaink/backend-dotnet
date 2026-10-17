using BackendDotNet.Application.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using BackendDotNet.Application.Abstractions.Messaging;

namespace BackendDotNet.Application.Abstractions.Messaging;

public sealed class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<Result<TResponse>> Send<TResponse>(ICommand<TResponse> command, CancellationToken ct = default)
        => Invoke<TResponse>(typeof(ICommandHandler<,>), command, ct);

    public Task<Result<TResponse>> Query<TResponse>(IQuery<TResponse> query, CancellationToken ct = default)
        => Invoke<TResponse>(typeof(IQueryHandler<,>), query, ct);

    private Task<Result<TResponse>> Invoke<TResponse>(Type openHandlerType, object message, CancellationToken ct)
    {
        var handlerType = openHandlerType.MakeGenericType(message.GetType(), typeof(TResponse));
        var handler = _serviceProvider.GetRequiredService(handlerType);
        var method = handlerType.GetMethod("Handle")!;
        return (Task<Result<TResponse>>)method.Invoke(handler, new object[] { message, ct })!;
    }
}   