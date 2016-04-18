using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HuddersfieldSportCentre.Models
{
    public enum Skills
    {
        Novice, Beginner, Intermediate, Advanced, Professional
    }

    public class Enrollment
    {
        
        public int EnrollmentID { get; set; }
        [Display(Name = "Course Title")]
        public int CourseID { get; set; }
        public int CustomerID { get; set; }
        public Skills? Skills { get; set; }

        public virtual Course Course { get; set; }
        public virtual Customer Customer { get; set; }
    }
}