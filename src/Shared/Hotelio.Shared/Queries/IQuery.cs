using System;
using MediatR;

namespace Hotelio.Shared.Queries;

public interface IQuery<T> : IRequest<T>
{

}


