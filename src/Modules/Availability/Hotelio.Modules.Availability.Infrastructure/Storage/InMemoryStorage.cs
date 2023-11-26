using Hotelio.Modules.Availability.Domain.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.Modules.Availability.Infrastructure.Storage;

internal class InMemoryStorage
{
    public static List<IDictionary<string, dynamic>> Resources { set; get; } = new List<IDictionary<string, dynamic>>()
    {
        new Dictionary<string, dynamic>()
        {
            {"Id", "id1"}, 
            {"GroupId", "Hotel-1"}, 
            {"Type", 1}, {"IsActive", true}, 
            {"Books", new List<IDictionary<string, dynamic>>() { new Dictionary<string, dynamic>() { {"Id", "test1"}, { "OwnerId", "owner-1"} } } }
        },
        new Dictionary<string, dynamic>()
        {
            {"Id", "id2"}, 
            {"GroupId", "Hotel-1"}, 
            {"Type", 2}, 
            {"IsActive", true}, 
            {"Books", new List<IDictionary<string, dynamic>>() { new Dictionary<string, dynamic>() { {"Id", "test2"} } } }
        },
        new Dictionary<string, dynamic>()
        {
            {"Id", "id3"}, 
            {"GroupId", "Hotel-2"}, 
            {"Type", 1}, 
            {"IsActive", true}, 
            {"Books", new List<IDictionary<string, dynamic>>() { new Dictionary<string, dynamic>() { {"Id", "test3"} } } }
        },
        new Dictionary<string, dynamic>()
        {
            {"Id", "id4"}, 
            {"GroupId", "Hotel-1"}, 
            {"Type", 2}, 
            {"IsActive", true}, 
            {"Books", new List<IDictionary<string, dynamic>>() { new Dictionary<string, dynamic>() { {"Id", "test4"} } } }
        }
    };
}