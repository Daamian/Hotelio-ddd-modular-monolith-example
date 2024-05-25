using Hotelio.CrossContext.Contract.HotelManagement.Event;
using Hotelio.Modules.Catalog.Core.Model;
using Hotelio.Modules.Catalog.Core.Repository;

namespace Hotelio.Modules.Catalog.Api.CrossContext.EventHandler;

internal class RoomEventHandler
{
    private readonly IHotelRepository _repository;

    public RoomEventHandler(IHotelRepository repository) => _repository = repository;

    public async Task Handle(RoomAdded contractEvent)
    {
        var hotel = await _repository.FindAsync(contractEvent.HotelId);

        if (hotel is null)
        {
            return;
        }
        
        hotel.Rooms.Add(new Room()
        {
            Id = contractEvent.RoomId,
            MaxGuests = contractEvent.MaxGuests,
            Type = contractEvent.Type
        });

        await _repository.UpdateAsync(hotel);
    }

    public async Task Handle(RoomUpdated contractEvent)
    {
        var hotel = await _repository.FindAsync(contractEvent.HotelId);
        
        if (hotel is null)
        {
            return;
        }

        var room = hotel.Rooms.Find(r => r.Id == contractEvent.RoomId);

        if (room is null)
        {
            hotel.Rooms.Add(new Room()
            {
                Id = contractEvent.RoomId,
                MaxGuests = contractEvent.MaxGuests,
                Type = contractEvent.Type
            });
        } else {
            room.MaxGuests = contractEvent.MaxGuests;
            room.Type = contractEvent.Type; 
        }

        await _repository.UpdateAsync(hotel);
    }
}