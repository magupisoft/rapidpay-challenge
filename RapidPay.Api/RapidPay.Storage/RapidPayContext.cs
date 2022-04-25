using Microsoft.EntityFrameworkCore;
using RapidPay.Storage.DbModel;

namespace RapidPay.Storage
{
    public class RapidPayContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<PayHistory> PayHistories { get; set; }
        public RapidPayContext(DbContextOptions<RapidPayContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>().HasKey(k => k.Id);
            modelBuilder.Entity<Card>().Property(k => k.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            modelBuilder.Entity<Card>().Property(k => k.Number).HasMaxLength(15);
            modelBuilder.Entity<Card>().Property(k => k.Number).IsRequired();
            modelBuilder.Entity<Card>().Property(k => k.Balance).IsRequired();


            modelBuilder.Entity<PayHistory>().HasKey(k => k.Id);
            modelBuilder.Entity<PayHistory>().Property(k => k.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            modelBuilder.Entity<Card>()
            .HasMany(c => c.PayHistories)
            .WithOne(e => e.Card);
        }
    }
}
