using Hotelio.Modules.HotelManagement.Core.DAL;
using Hotelio.Modules.HotelManagement.Core.DAL.Repository;
using Hotelio.Modules.HotelManagement.Core.Service;
using Hotelio.Shared.Tests;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.HotelManagement.Test.Integration.Fixture;

public class DatabaseFixture : IDisposable
{
    private readonly HotelDbContext _dbContext;
    
    public DatabaseFixture()
    {
        var optionBuilder = new DbContextOptionsBuilder<HotelDbContext>()
            .UseSqlServer(ConfigHelper.GetSqlServerConfig().ConnectionString);
        _dbContext = new HotelDbContext(optionBuilder.Options);
        _dbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
    }
}