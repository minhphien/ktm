using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using KMS.Product.Ktm.Repository.Models;

namespace KMS.Product.Ktm.Repository
{
    public class KtmContext : DbContext
    {
        public KtmContext()
        {
        }
        public KtmContext(string connString) : base(connString)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<EmployeeTeam> EmployeeTeams { get; set; }
        public DbSet<KudoType> KudoTypes { get; set; }
        public DbSet<Kudo> Kudos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kudo>()
                    .HasRequired(m => m.Sender)
                    .WithMany(t => t.KudoSends)
                    .HasForeignKey(m => m.SenderID)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kudo>()
                    .HasRequired(m => m.Receiver)
                    .WithMany(t => t.KudoReceives)
                    .HasForeignKey(m => m.ReceiverID)
                    .WillCascadeOnDelete(false);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
