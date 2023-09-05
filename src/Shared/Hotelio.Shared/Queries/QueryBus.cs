﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.Shared.Queries;

internal sealed class QueryBus : IQueryBus
{
    private readonly IServiceProvider _serviceProvider;

    public QueryBus(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        var method = handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync));
        if (method is null)
        {
            throw new InvalidOperationException($"Query handler for '{typeof(TResult).Name}' is invalid.");
        }

        // ReSharper disable once PossibleNullReferenceException
        return await (Task<TResult>)method.Invoke(handler, new object[] { query });
    }
}


