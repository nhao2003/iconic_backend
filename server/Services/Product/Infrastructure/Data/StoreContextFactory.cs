using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public class StoreContextFactory(IConfigurationRoot configuration) : IDesignTimeDbContextFactory<StoreContext>
{
    public StoreContext CreateDbContext(string[] args)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new StoreContext(optionsBuilder.Options);
    }
}