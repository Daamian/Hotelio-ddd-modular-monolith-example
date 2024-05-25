using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Domain.Repository;
using MediatR;

namespace Hotelio.Modules.Availability.Application.Command.Handlers;

internal class CreateHandler: IRequestHandler<Create>
{
    private readonly IResourceRepository _repository;

    public CreateHandler(IResourceRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(Create request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(Resource.Create(request.Id, request.ExternalId, request.GroupId, request.Type));
    }
}