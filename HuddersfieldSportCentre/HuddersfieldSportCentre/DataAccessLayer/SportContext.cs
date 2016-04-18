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
        public DbSet<Department> Departments { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        // public DbSet<CourtAssignment> CourtAssignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Course>()
             .HasMany(a => a.Trainers).WithMany(i => i.Courses)
             .Map(t => t.MapLeftKey("CourseID")
                 .MapRightKey("TrainerID")
                 .ToTable("CourseTrainer"));
        }
    }
}