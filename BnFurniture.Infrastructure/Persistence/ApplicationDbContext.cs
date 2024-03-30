using BnFurniture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BnFurniture.Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        // public DbSet<ExampleEntity> ExampleEntities { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public async ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default, [CallerMemberName] string? callerFunction = null, [CallerFilePath] string? callerFile = null) =>
        await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
