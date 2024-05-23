using Hotelio.Modules.Availability.Infrastructure.DAL;
using Hotelio.Shared.Tests;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.Availability.Test.Integration.Fixture;

public class DatabaseFixture
{
    private readonly ResourceDbContext _dbContext;
    
    public DatabaseFixture()
    {
        var optionBuilder = new DbContextOptionsBuilder<ResourceDbContext>()
            .UseSqlServer(ConfigHelper.GetSqlServerConfig().ConnectionString);
        _dbContext = new ResourceDbContext(optionBuilder.Options);
        _dbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
    }
}