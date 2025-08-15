using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SelkaWraps.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Buyer -> Orders (1-to-many)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Buyer)
                .WithMany(b => b.Orders)
                .HasForeignKey(o => o.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Listing -> Orders (1-to-many)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Listing)
                .WithMany(l => l.Orders)
                .HasForeignKey(o => o.ListingId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }


}
