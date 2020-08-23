using HouseHub.Web.UserApi.Core.Enums;
using HouseHub.Web.UserApi.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HouseHub.Web.UserApi.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Worker> Workers { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetModelsRelations(modelBuilder);
            SetIndexes(modelBuilder);
            SetDiscriminators(modelBuilder);
        }

        private static void SetModelsRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasOne(t => t.User)
                .WithMany(t => t.Accounts);

            modelBuilder.Entity<Domain>()
                .HasOne(t => t.Worker)
                .WithMany(t => t.Domains);

            modelBuilder.Entity<Location>()
                .HasOne(t => t.Customer)
                .WithMany(t => t.Locations);
        }

        private static void SetDiscriminators(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasDiscriminator<AccountType>("Type")
                .HasValue<Customer>(AccountType.Customer)
                .HasValue<Worker>(AccountType.Worker);
        }

        private static void SetIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(t => t.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(t => t.PhoneNumber)
                .IsUnique();
        }
    }
}