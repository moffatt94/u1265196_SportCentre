using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HuddersfieldSportCentre.Models;

namespace HuddersfieldSportCentre.DataAccessLayer
{
    public class SportInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SportContext>
    {
        protected override void Seed(SportContext context)
        {
            var customers = new List<Customer>
            {
            new Customer{FirstName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Customer{FirstName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Customer{FirstName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Customer{FirstName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Customer{FirstName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Customer{FirstName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Customer{FirstName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Customer{FirstName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };

            customers.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();
            var courses = new List<Course>
            {
            new Course{CourseID=1050,Title="Football",Level=1,},
            new Course{CourseID=4022,Title="Tennis",Level=2,},
            new Course{CourseID=4041,Title="Cricket",Level=3,},
            new Course{CourseID=1045,Title="Badminton",Level=4,},
            new Course{CourseID=3141,Title="Squash",Level=2,},
            new Course{CourseID=2021,Title="Swimming",Level=3,},
            new Course{CourseID=2042,Title="PingPong",Level=1,}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();
            var enrollments = new List<Enrollment>
            {
            new Enrollment{CustomerID=1,CourseID=1050,Skills=Skills.Advanced},
            new Enrollment{CustomerID=1,CourseID=4022,Skills=Skills.Beginner},
            new Enrollment{CustomerID=1,CourseID=4041,Skills=Skills.Intermediate},
            new Enrollment{CustomerID=2,CourseID=1045,Skills=Skills.Advanced},
            new Enrollment{CustomerID=2,CourseID=3141,Skills=Skills.Novice},
            new Enrollment{CustomerID=2,CourseID=2021,Skills=Skills.Professional},
            new Enrollment{CustomerID=3,CourseID=1050},
            new Enrollment{CustomerID=4,CourseID=1050,},
            new Enrollment{CustomerID=4,CourseID=4022,Skills=Skills.Intermediate},
            new Enrollment{CustomerID=5,CourseID=4041,Skills=Skills.Beginner},
            new Enrollment{CustomerID=6,CourseID=1045},
            new Enrollment{CustomerID=7,CourseID=3141,Skills=Skills.Intermediate},
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}