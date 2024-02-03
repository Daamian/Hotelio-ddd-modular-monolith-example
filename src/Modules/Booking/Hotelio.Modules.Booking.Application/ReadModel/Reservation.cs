using Hotelio.Modules.Booking.Application.ReadModel.VO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hotelio.Modules.Booking.Application.ReadModel;

internal class Reservation
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; }
    public Hotel Hotel { get; set; }
    public Owner Owner { get; set; }
    public RoomType RoomType { get; set; }
    public int NumberOfGuests { get; set; }
    public string Status { get; set; }
    public double PriceToPay { get; set; }
    public double PricePayed { get; set; }
    public string PaymentType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Amenity> Amenities { get; set; }
    public string? RoomId { get; set; } = null;

    public Reservation(string id, Hotel hotel, Owner owner, RoomType roomType, int numberOfGuests, string status, double priceToPay, double pricePayed, string paymentType, DateTime startDate, DateTime endDate, List<Amenity> amenities, string? roomId = null)
    {
        Id = id;
        Hotel = hotel;
        Owner = owner;
        RoomType = roomType;
        NumberOfGuests = numberOfGuests;
        Status = status;
        PriceToPay = priceToPay;
        PricePayed = pricePayed;
        PaymentType = paymentType;
        StartDate = startDate;
        EndDate = endDate;
        Amenities = amenities;
        RoomId = roomId;
    }
}




