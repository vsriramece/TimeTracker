using Reviso.TimeTracker.UI.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reviso.TimeTracker.UI.Models
{
    public class TimeEntryIndexViewModel
    {
        public IEnumerable<TimeEntryModel> TimeEntries { get; set; }
    }

    public class TimeEntryModel
    {
        [Display(Name = nameof(Resources.TimeTracker_Index_EntryDate), ResourceType = typeof(Resources))]
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public decimal Hours { get; set; }

        public Guid Id { get; set; }
    }
}