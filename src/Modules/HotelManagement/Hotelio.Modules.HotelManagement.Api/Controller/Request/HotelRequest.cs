namespace Hotelio.Modules.HotelManagement.Api.Controller.Request;

internal record HotelRequest(string Name, List<int>? Amenities);