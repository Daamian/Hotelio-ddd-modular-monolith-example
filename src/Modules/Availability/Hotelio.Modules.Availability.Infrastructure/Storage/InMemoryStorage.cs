namespace Hotelio.Modules.Availability.Infrastructure.Storage;

internal class InMemoryStorage
{
    public static List<IDictionary<string, dynamic>> Resources { set; get; } = new List<IDictionary<string, dynamic>>()
    {
        new Dictionary<string, dynamic>()
        {
            {"Id", "ccac553d-d50d-4785-806a-7e32fdea3c23"}, 
            {"GroupId", "Hotel-1"}, 
            {"Type", 1}, {"IsActive", true}, 
            {"Books", new List<IDictionary<string, dynamic>>() { new Dictionary<string, dynamic>() } }
        },
        new Dictionary<string, dynamic>()
        {
            {"Id", "11f41341-5ef6-4942-af4b-ab2c700db8c5"}, 
            {"GroupId", "Hotel-1"}, 
            {"Type", 2}, 
            {"IsActive", true}, 
            {"Books", new List<IDictionary<string, dynamic>>() { new Dictionary<string, dynamic>() } }
        },
        new Dictionary<string, dynamic>()
        {
            {"Id", "1cd46654-44df-454b-9b03-24f58fb226dd"}, 
            {"GroupId", "Hotel-2"}, 
            {"Type", 1}, 
            {"IsActive", true}, 
            {"Books", new List<IDictionary<string, dynamic>>() { new Dictionary<string, dynamic>() } }
        },
        new Dictionary<string, dynamic>()
        {
            {"Id", "cbf8691d-02d1-481d-9263-f379b6cfbfc4"}, 
            {"GroupId", "Hotel-1"}, 
            {"Type", 2}, 
            {"IsActive", true}, 
            {"Books", new List<IDictionary<string, dynamic>>() { new Dictionary<string, dynamic>() } }
        }
    };
}