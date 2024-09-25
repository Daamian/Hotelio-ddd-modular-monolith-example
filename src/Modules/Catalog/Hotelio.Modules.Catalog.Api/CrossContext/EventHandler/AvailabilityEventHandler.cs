using Hotelio.CrossContext.Contract.Availability.Event;
using Hotelio.Modules.Catalog.Core.Model;
using Hotelio.Modules.Catalog.Core.Repository;
using MassTransit;
using MediatR;

namespace Hotelio.Modules.Catalog.Api.CrossContext.EventHandler;

internal class AvailabilityEventHandler: IConsumer<ResourceBooked>
{
    private readonly IHotelRepository _repository;

    public AvailabilityEventHandler(IHotelRepository repository)
    {
        _repository = repository;
    }
    
    public async Task HandleUnBook()
    {
        //TODO implement event and remove reservation
    }

    public async Task Consume(ConsumeContext<ResourceBooked> context)
    {
        var contractEvent = context.Message;
        var hotel = await _repository.FindByRoomAsync(contractEvent.ResourceId);
        var room = hotel.Rooms.Find(r => r.Id == contractEvent.ResourceId);

        if (room is null)
        {
            return;
        }
        
        room.Reservations.Add(new Reservation
        {
            StartDate = contractEvent.StartDate,
            StopDate = contractEvent.EndDate
        });

        await _repository.UpdateAsync(hotel);
    }
}