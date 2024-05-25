using Hotelio.CrossContext.Contract.HotelManagement.Event;
using Hotelio.Modules.Catalog.Core.Model;
using Hotelio.Modules.Catalog.Core.Repository;

namespace Hotelio.Modules.Catalog.Api.CrossContext.EventHandler;

internal class HotelEventHandler
{
    private readonly IHotelRepository _repository;

    public HotelEventHandler(IHotelRepository repository) => _repository = repository;
    
    public async Task Handle(HotelCreated contractEvent)
    {
        await _repository.AddAsync(new Hotel()
        {
            Id = contractEvent.Id,
            Name = contractEvent.Name
        });
    }
    
    public async Task Handle(HotelUpdated contractEvent)
    {
        var hotel = await _repository.FindAsync(contractEvent.Id);

        if (null == hotel)
        {
            await _repository.AddAsync(new Hotel()
            {
                Id = contractEvent.Id,
                Name = contractEvent.Name
            });
        }
        else
        {
            hotel.Name = contractEvent.Name;
            await _repository.UpdateAsync(hotel);
        }
    }
}