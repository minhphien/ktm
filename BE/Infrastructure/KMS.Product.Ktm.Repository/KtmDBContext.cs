
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Repository
{
    public class KtmDbContext : DbContext
    {
        public KtmDbContext()
        {
        }
        public KtmDbContext(string connString) : base(connString)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<EmployeeTeam> EmployeeTeams { get; set; }
        public DbSet<KudoType> KudoTypes { get; set; }
        public DbSet<Kudo> Kudos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<int>()
                .Where(p => p.Name.Equals("Id"))
                .Configure(c => c.HasColumnName(c.ClrPropertyInfo.ReflectedType.Name + "Id"));
            modelBuilder.Entity<Kudo>()
                    .HasRequired(m => m.Sender)
                    .WithMany(t => t.KudoSends)
                    .HasForeignKey(m => m.SenderId)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kudo>()
                    .HasRequired(m => m.Receiver)
                    .WithMany(t => t.KudoReceives)
                    .HasForeignKey(m => m.ReceiverId)
                    .WillCascadeOnDelete(false);

        }

        public override int SaveChanges()
        {
            AddAuitInfo();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            AddAuitInfo();
            return await base.SaveChangesAsync();
        }

        private void AddAuitInfo()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
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
