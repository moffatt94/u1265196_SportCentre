using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuddersfieldSportCentre.Models
{
    public class CourtAssignment
    {
        [Key]
        [ForeignKey("Trainer")]
        public int CourseID { get; set; }
        [Display(Name = "Court Location")]
        public string Location { get; set; }

        public virtual Trainer Trainer { get; set; }
    }
}