using System;
using System.Collections.Generic;
using System.Linq;

namespace Reviso.TimeTracker.UI.Models
{
    public class ReportSummary
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<TimeEntryModel> TimeEntries { get; set; }
        public List<ProjectSummary> ProjectSummary { get; set; }
        public decimal TotalHours => ProjectSummary?.Sum(o => o.TotalHours)??0;
    }
}