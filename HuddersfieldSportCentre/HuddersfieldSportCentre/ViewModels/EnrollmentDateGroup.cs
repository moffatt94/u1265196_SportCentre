using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HuddersfieldSportCentre.ViewModels
{
    public class EnrollmentDateGroup
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EnrollmentDate { get; set; }

        public int CustomerCount { get; set; }

    }
}