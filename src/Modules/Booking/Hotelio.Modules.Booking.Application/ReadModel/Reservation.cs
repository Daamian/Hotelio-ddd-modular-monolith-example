using System;
using System.Runtime.CompilerServices;
using Hotelio.Modules.Booking.Application.ReadModel.VO;

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Hotelio.Modules.Booking.Api")]
namespace Hotelio.Modules.Booking.Application.ReadModel;

internal class Reservation
{
    public Guid Id { set; get; }
    public Hotel Hotel { set; get; }
    public RoomType RoomType { set; get; }
    public int NumberOfGuests { set; get; }
    public String Status { set; get; }
    public double PriceToPay { set; get; }
    public double PricePayed { set; get; }
    public String PaymentType { set; get; }
    public DateTime StartDate { set; get; }
    public DateTime EndDate { set; get; }
    public List<Amenity> Amenities { set; get; }

    public Reservation(Guid id, Hotel hotel, RoomType roomType, int numberOfGuests, string status, double priceToPay, double pricePayed, string paymentType, DateTime startDate, DateTime endDate, List<Amenity> amenities)
    {
        Id = id;
        Hotel = hotel;
        RoomType = roomType;
        NumberOfGuests = numberOfGuests;
        Status = status;
        PriceToPay = priceToPay;
        PricePayed = pricePayed;
        PaymentType = paymentType;
        StartDate = startDate;
        EndDate = endDate;
        Amenities = amenities;
    }
}


