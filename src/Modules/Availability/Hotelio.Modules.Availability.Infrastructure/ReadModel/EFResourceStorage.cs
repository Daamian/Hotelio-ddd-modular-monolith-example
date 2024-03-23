using System.Data;
using System.Data.Common;
using Hotelio.Modules.Availability.Application.ReadModel;
using Hotelio.Modules.Availability.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;


namespace Hotelio.Modules.Availability.Infrastructure.ReadModel;

internal class EFResourceStorage : IResourceStorage
{
    private readonly ResourceDbContext _dbc;
    public EFResourceStorage(ResourceDbContext dbContext)
    {
        _dbc = dbContext;
    }
    
    public Task<Resource?> FindFirstAvailableInDatesAsync(string group, int type, DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }

    public void testQuery()
    {
        /*var ids = _dbc.Database
            .SqlQuery<int>($"SELECT * FROM availability.Resources")
            .ToList();*/
        
        var r1 = _dbc.Resources.FromSql($"SELECT * FROM availability.Resources").AsNoTracking().ToList();
        var r2 = _dbc.Resources.FromSqlRaw($"SELECT * FROM availability.Resources").ToList();

        var a = 1;
    }
}