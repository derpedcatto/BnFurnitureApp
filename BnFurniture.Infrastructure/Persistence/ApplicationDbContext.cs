using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BnFurniture.Infrastructure.Persistence
{
    public interface IDbContext : IDisposable
    {
        /*
        Пример:
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        */

        ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default, [CallerMemberName] string? callerFunction = null, [CallerFilePath] string? callerFile = null);
    }

    public class ApplicationDbContext(DbContextOptions options) : DbContext(options), IDbContext
    {
        // Реализация интерфейса

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
