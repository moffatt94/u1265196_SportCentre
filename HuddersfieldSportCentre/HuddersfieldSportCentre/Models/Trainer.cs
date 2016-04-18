using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuddersfieldSportCentre.Models
{
    public class Trainer
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Started Job")]
        public DateTime StartedDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }

        public virtual ICollection<Course> Courses { get; set; }
       // public virtual CourtAssignment CourtAssignment { get; set; }
    }
}