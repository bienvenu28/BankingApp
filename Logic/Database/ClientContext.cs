using System.Reflection;
using BankingApp.Logic.Model;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Logic.Database
{
    class ClienContext : DbContext
    {
        public DbSet<Client> ClientList { get; set; }
        //Configuration du contexte
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=BankDatabase.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        //Mapping du modèle 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Client>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.FirstName);
                e.Property(e => e.LastName);
                e.Property(e => e.Amount).HasDefaultValue(0.0);
                e.Property(e => e.Pin);
                e.Property(e => e.IsBlocked).HasDefaultValue(false);
            });
            modelBuilder.Entity("BankingApp.Logic.Model.Client").HasData(
                new Client
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Dylan",
                    Amount = 200.2,
                    Pin = "12340"
                },
                new Client
                {
                    Id = 2,
                    FirstName = "Remi",
                    LastName = "Gold",
                    Amount = 200000.2,
                    Pin = "4560"
                },
                new Client
                {
                    Id = 3,
                    FirstName = "Bobby",
                    LastName = "Haang",
                    Amount = 100000,
                    Pin = "7777"
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
