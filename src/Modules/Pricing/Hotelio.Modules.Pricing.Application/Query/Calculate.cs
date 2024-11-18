using MediatR;

namespace Hotelio.Modules.Pricing.Application.Query;

internal record Calculate(string HotelId, string RoomType, List<string> Amenities, DateTime StartDate, DateTime EndDate) : IRequest<double>;