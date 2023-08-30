using System;
namespace Hotelio.Shared.Queries;

public interface IQueryBus
{
    public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
}


