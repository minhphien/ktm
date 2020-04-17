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
        public DbSet<CheckListItem> CheckListItems { get; set; }
        public DbSet<CheckListStatus> CheckListStatus { get; set; }
        public DbSet<CheckListAssign> CheckListAssigns { get; set; }

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
                .HasOne(a => a.Sender)
                .WithMany(a => a.KudoSends)
                .HasForeignKey(a => a.SenderId);

            modelBuilder.Entity<Kudo>()
                .HasOne(a => a.Receiver)
                .WithMany(a => a.KudoReceives)
                .HasForeignKey(a => a.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Mentor)
                .WithMany(a => a.Mentee)
                .HasForeignKey(a => a.MentorId);

            modelBuilder.Entity<EmployeeRole>()
                .HasData(
                new EmployeeRole {Id = 1, Created = new DateTime(2020, 4, 1), Modified = new DateTime(2020, 4, 1), RoleName = "Default"});

            modelBuilder.Entity<KudoType>()
                .HasData(
                new KudoType { Id = 1, Created = new DateTime(2020, 4, 1), Modified = new DateTime(2020, 4, 1), TypeName = "Default" });

            modelBuilder.Entity<CheckListStatus>()
                .HasData(
                new CheckListStatus { Id = 1, Created = new DateTime(2020, 4, 1), Modified = new DateTime(2020, 4, 1), Status = "To Do" },
                new CheckListStatus { Id = 2, Created = new DateTime(2020, 4, 1), Modified = new DateTime(2020, 4, 1), Status = "In Progress" },
                new CheckListStatus { Id = 3, Created = new DateTime(2020, 4, 1), Modified = new DateTime(2020, 4, 1), Status = "Done" });
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
