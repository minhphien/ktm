using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Repository
{
    public class KtmDbContext : DbContext
    {
        private const string Id = "Id";

        public KtmDbContext(DbContextOptions<KtmDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<EmployeeTeam> EmployeeTeams { get; set; }
        public DbSet<KudoType> KudoTypes { get; set; }
        public DbSet<Kudo> Kudos { get; set; }
        public DbSet<KudoDetail> KudoDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(int)))
            {
                if (property.GetColumnName().Equals(Id))
                    property.SetColumnName(property.PropertyInfo.ReflectedType.Name + Id);
            }

            modelBuilder.Entity<Kudo>()
                .HasOne<EmployeeTeam>(a => a.Sender)
                .WithOne()
                .HasForeignKey<Kudo>(a => a.SenderId);

            modelBuilder.Entity<Kudo>()
                .HasOne<EmployeeTeam>(a => a.Receiver)
                .WithOne()
                .HasForeignKey<Kudo>(a => a.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public override int SaveChanges()
        {
            AddAuitInfo();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddAuitInfo();
            return await base.SaveChangesAsync();
        }

        private void AddAuitInfo()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).Created = DateTime.Now;
                }
                ((BaseEntity)entry.Entity).Modified = DateTime.Now;
            }

        }
    }
}
