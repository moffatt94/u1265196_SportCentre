using HuddersfieldSportCentre.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HuddersfieldSportCentre.DataAccessLayer
{
    public class SportContext : DbContext
    {

        public SportContext()
            : base("SportContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}