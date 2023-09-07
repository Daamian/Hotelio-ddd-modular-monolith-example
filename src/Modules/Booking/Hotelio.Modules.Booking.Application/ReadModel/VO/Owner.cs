namespace Hotelio.Modules.Booking.Application.ReadModel.VO;

public class Owner
{
    public string Id { set; get; }
    public string Name { set; get; }
    public string Surname { set; get; }

    public Owner(string id, string name, string surname)
    {
        Id = id;
        Name = name;
        Surname = surname;
    }
}