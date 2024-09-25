using Hotelio.CrossContext.Contract.HotelManagement.Event;
using Hotelio.Modules.Catalog.Core.Model;
using Hotelio.Modules.Catalog.Core.Repository;
using MassTransit;
using MediatR;

namespace Hotelio.Modules.Catalog.Api.CrossContext.EventHandler;

internal class HotelEventHandler: IConsumer<HotelCreated>, IConsumer<HotelUpdated>
{
    private readonly IHotelRepository _repository;

    public HotelEventHandler(IHotelRepository repository) => _repository = repository;

    public async Task Consume(ConsumeContext<HotelCreated> context)
    {
        var contractEvent = context.Message;
        await _repository.AddAsync(new Hotel
        {
            Id = contractEvent.Id,
            Name = contractEvent.Name
        });
    }

    public async Task Consume(ConsumeContext<HotelUpdated> context)
    {
        var contractEvent = context.Message;
        var hotel = await _repository.FindAsync(contractEvent.Id);

        if (null == hotel)
        {
            await _repository.AddAsync(new Hotel
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