using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reviso.TimeTracker.UI.Models
{
    public class ReportFilter
    {
        [DisplayName("Start Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}