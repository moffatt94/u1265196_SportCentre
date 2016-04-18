namespace HuddersfieldSportCentre.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HuddersfieldSportCentre.Models;
    using HuddersfieldSportCentre.ViewModels;
    using HuddersfieldSportCentre.DataAccessLayer;

    internal sealed class Configuration : DbMigrationsConfiguration<HuddersfieldSportCentre.DataAccessLayer.SportContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HuddersfieldSportCentre.DataAccessLayer.SportContext context)
        {
            var customers = new List<Customer>
            {
            new Customer{FirstName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Customer{FirstName="Jozell",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Customer{FirstName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Customer{FirstName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Customer{FirstName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Customer{FirstName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Customer{FirstName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Customer{FirstName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };

            customers.ForEach(s => context.Customers.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var trainers = new List<Trainer>
            {
                new Trainer { FirstName = "Kim",     LastName = "Abercrombie", 
                    StartedDate = DateTime.Parse("1995-03-11") },
                new Trainer { FirstName = "Fadi",    LastName = "Fakhouri",    
                    StartedDate = DateTime.Parse("2002-07-06") },
                new Trainer { FirstName = "Roger",   LastName = "Harui",       
                    StartedDate = DateTime.Parse("1998-07-01") },
                new Trainer { FirstName = "Candace", LastName = "Kapoor",      
                    StartedDate = DateTime.Parse("2001-01-15") },
                new Trainer { FirstName = "Roger",   LastName = "Zheng",      
                    StartedDate = DateTime.Parse("2004-02-12") }
            };
            trainers.ForEach(s => context.Trainers.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department { Name = "Football",     Budget = 350000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    TrainerID  = trainers.Single( i => i.LastName == "Abercrombie").ID },
                new Department { Name = "Swimming", Budget = 100000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    TrainerID  = trainers.Single( i => i.LastName == "Fakhouri").ID },
                new Department { Name = "Tennis", Budget = 350000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    TrainerID  = trainers.Single( i => i.LastName == "Harui").ID },
                new Department { Name = "Cricket",   Budget = 100000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    TrainerID  = trainers.Single( i => i.LastName == "Kapoor").ID }
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var courses = new List<Course>
            {
            new Course{CourseID=1050,Title="Beginner Football",
                DepartmentID = departments.Single( s => s.Name == "Football").DepartmentID,
                Trainers = new List<Trainer>() 
            },

            new Course{CourseID=4022,Title="Intermediate Tennis",
            DepartmentID = departments.Single( s => s.Name == "Tennis").DepartmentID,
                Trainers = new List<Trainer>() 
            },
            new Course{CourseID=4041,Title="Advanced Cricket",
                DepartmentID = departments.Single( s => s.Name == "Cricket").DepartmentID,
                Trainers = new List<Trainer>() 
            },
            new Course{CourseID=1045,Title="Novice Badminton",
                DepartmentID = departments.Single( s => s.Name == "Tennis").DepartmentID,
                Trainers = new List<Trainer>() 
            },
            new Course{CourseID=3141,Title="Intermediate Squash",
                DepartmentID = departments.Single( s => s.Name == "Tennis").DepartmentID,
                Trainers = new List<Trainer>() 
            },
            new Course{CourseID=2021,Title="Advanced Swimming",
                DepartmentID = departments.Single( s => s.Name == "Swimming").DepartmentID,
                Trainers = new List<Trainer>() 
            },
            new Course{CourseID=2042,Title="Beginner PingPong",
                DepartmentID = departments.Single( s => s.Name == "Cricket").DepartmentID,
                Trainers = new List<Trainer>() 
            }
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID, s));
            context.SaveChanges();

          // var courtAssignments = new List<CourtAssignment>
            //{
              //  new CourtAssignment { 
                ///    CourseID = courses.Single( i => i.Title == "Advanced Swimming").CourseID, 
                   // Location = "Smith 17" },
                //new CourtAssignment { 
                  //  CourseID = courses.Single( i => i.Title == "Intermediate Tennis").CourseID, 
                    //Location = "Gowan 27" },
                //new CourtAssignment { 
                  //  CourseID = courses.Single( i => i.Title == "Advanced Football").CourseID, 
                    //Location = "Thompson 304" },
            //};
            //courtAssignments.ForEach(s => context.CourtAssignments.AddOrUpdate(p => p.CourseID, s));
            //context.SaveChanges(); 

            AddOrUpdateTrainer(context, "Beginner Football", "Kapoor");
            AddOrUpdateTrainer(context, "Advanced Cricket", "Harui");
            AddOrUpdateTrainer(context, "Novice Badminton", "Zheng");
            AddOrUpdateTrainer(context, "Advanced Swimming", "Zheng");

            AddOrUpdateTrainer(context, "Beginner Ping Pong", "Fakhouri");
            AddOrUpdateTrainer(context, "Intermediate Squash", "Harui");
            AddOrUpdateTrainer(context, "Beginner Football", "Abercrombie");
            AddOrUpdateTrainer(context, "Advanced Cricket", "Abercrombie");

            context.SaveChanges();



            var enrollments = new List<Enrollment>
            {
            new Enrollment{CustomerID = customers.Single(s => s.LastName == "Alexander").ID, 
                    CourseID = courses.Single(c => c.Title == "Beginner Football" ).CourseID},

            new Enrollment{CustomerID = customers.Single(s => s.LastName == "Alexander").ID, 
                    CourseID = courses.Single(c => c.Title == "Advanced Cricket" ).CourseID},

            new Enrollment{CustomerID = customers.Single(s => s.LastName == "Alexander").ID, 
                    CourseID = courses.Single(c => c.Title == "Beginner PingPong" ).CourseID},

            new Enrollment{CustomerID = customers.Single(s => s.LastName == "Alonso").ID, 
                    CourseID = courses.Single(c => c.Title == "Advanced Swimming" ).CourseID},

            new Enrollment{CustomerID = customers.Single(s => s.LastName == "Alonso").ID, 
                    CourseID = courses.Single(c => c.Title == "Intermediate Squash" ).CourseID},

           new Enrollment{CustomerID = customers.Single(s => s.LastName == "Alonso").ID, 
                    CourseID = courses.Single(c => c.Title == "Beginner Football" ).CourseID},

            new Enrollment{CustomerID = customers.Single(s => s.LastName == "Barzdukas").ID, 
                    CourseID = courses.Single(c => c.Title == "Beginner Football" ).CourseID},

            new Enrollment{CustomerID = customers.Single(s => s.LastName == "Barzdukas").ID, 
                    CourseID = courses.Single(c => c.Title == "Advanced Cricket" ).CourseID},

            new Enrollment{CustomerID = customers.Single(s => s.LastName == "Li").ID, 
                    CourseID = courses.Single(c => c.Title == "Beginner Football" ).CourseID},

            new Enrollment{CustomerID = customers.Single(s => s.LastName == "Justice").ID, 
                    CourseID = courses.Single(c => c.Title == "Advanced Cricket" ).CourseID},


            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                         s.Customer.ID == e.CustomerID &&
                         s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }

        void AddOrUpdateTrainer(SportContext context, string courseTitle, string trainerName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = crs.Trainers.SingleOrDefault(i => i.LastName == trainerName);
            if (inst == null)
                crs.Trainers.Add(context.Trainers.Single(i => i.LastName == trainerName));
        }
    }
}