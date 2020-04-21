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
        public DbSet<Status> Status { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<CheckListItem> CheckListItems { get; set; }
        public DbSet<CheckList> CheckLists { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentItem> AssignmentItems { get; set; }

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

            modelBuilder.Entity<Item>()
                .HasOne(a => a.Creator)
                .WithMany(a => a.CheckListItems)
                .HasForeignKey(a => a.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssignmentItem>()
                .HasOne(a => a.Status)
                .WithMany(a => a.AssignmentItems)
                .HasForeignKey(a => a.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Status)
                .WithMany(a => a.Assignments)
                .HasForeignKey(a => a.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeRole>()
                .HasData(
                new EmployeeRole {Id = 1, Created = new DateTime(2020, 4, 1), Modified = new DateTime(2020, 4, 1), RoleName = "Default"});

            modelBuilder.Entity<KudoType>()
                .HasData(
                new KudoType { Id = 1, Created = new DateTime(2020, 4, 1), Modified = new DateTime(2020, 4, 1), TypeName = "Default" });

            modelBuilder.Entity<Status>()
                .HasData(
                new Status { Id = 1, Created = new DateTime(2020, 4, 1), Modified = new DateTime(2020, 4, 1), Name = "To Do" },
                new Status { Id = 2, Created = new DateTime(2020, 4, 1), Modified = new DateTime(2020, 4, 1), Name = "In Progress" },
                new Status { Id = 3, Created = new DateTime(2020, 4, 1), Modified = new DateTime(2020, 4, 1), Name = "Done" });
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
