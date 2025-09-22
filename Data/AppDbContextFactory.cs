using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace UserManagement.API.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);

            var config = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var conn = config.GetConnectionString("DefaultConnection") ?? "Data Source=users.db";
            optionsBuilder.UseSqlite(conn);
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
