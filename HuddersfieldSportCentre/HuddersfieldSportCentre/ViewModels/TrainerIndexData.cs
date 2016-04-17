using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HuddersfieldSportCentre.Models;

namespace HuddersfieldSportCentre.ViewModels
{
    public class TrainerIndexData
    {
        public IEnumerable<Trainer> Trainers { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}