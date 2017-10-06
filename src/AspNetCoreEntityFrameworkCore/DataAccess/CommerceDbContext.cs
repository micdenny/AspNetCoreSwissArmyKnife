using AspNetCoreEntityFrameworkCore.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreEntityFrameworkCore.DataAccess
{
    public class CommerceDbContext : DbContext
    {
        public CommerceDbContext(DbContextOptions<CommerceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
                .HasKey(x => new { x.CartId, x.ProductId });
        }
    }
}
