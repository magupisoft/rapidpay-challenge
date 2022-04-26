using Microsoft.EntityFrameworkCore;
using RapidPay.Storage.DbModel;
using System;
using RapidPay.Domain.Extensions;

namespace RapidPay.Storage
{
    public class RapidPayContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<PayHistory> PayHistories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PaymentFee> PaymentFees { get; set; }
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
            modelBuilder.Entity<Card>()
               .HasIndex(u => u.Number)
               .IsUnique();

            modelBuilder.Entity<PayHistory>().HasKey(k => k.Id);
            modelBuilder.Entity<PayHistory>().Property(k => k.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            modelBuilder.Entity<Card>()
            .HasMany(c => c.PayHistories)
            .WithOne(e => e.Card);

            modelBuilder.Entity<User>().HasKey(k => k.Id);
            modelBuilder.Entity<User>().Property(k => k.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            modelBuilder.Entity<User>().Property(k => k.Username).IsRequired();
            modelBuilder.Entity<User>().Property(k => k.Password).IsRequired();

            modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "user1", Password = "pwd12345" });


            modelBuilder.Entity<PaymentFee>().HasKey(k => k.Id);
            modelBuilder.Entity<PaymentFee>().Property(k => k.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            modelBuilder.Entity<PaymentFee>().Property(k => k.Fee).IsRequired();
            modelBuilder.Entity<PaymentFee>().Property(k => k.LastUpdated).IsRequired();

            var rand = new Random();
            modelBuilder.Entity<PaymentFee>().HasData(
            new PaymentFee { Id = 1, Fee = rand.NextDecimal(0.0, 2.0), LastUpdated = DateTime.UtcNow });
        }
    }
}
