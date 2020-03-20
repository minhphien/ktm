using System.Linq;
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
        public DbSet<Team> Teams { get; set; }
        public DbSet<EmployeeTeam> EmployeeTeams { get; set; }
        public DbSet<KudoType> KudoTypes { get; set; }
        public DbSet<Kudo> Kudos { get; set; }

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
       
    }
}
