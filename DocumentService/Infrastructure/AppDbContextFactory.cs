using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DocumentService.Infrastructure.Data;

namespace DocumentService.Infrastructure
{
    public class AppDbContextFactory
        : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(
            string[] args)
        {
            var optionsBuilder =
                new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=(local)\\SQLEXPRESS;Database=AIResumeDb;Trusted_Connection=True;TrustServerCertificate=True"
            );

            return new AppDbContext(
                optionsBuilder.Options);
        }
    }
}