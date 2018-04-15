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

        [StringLength(50, MinimumLength = 3)] //For now, setting the max length as 50 and Min length as 3
        [Display(Name = nameof(Resources.TimeTracker_Index_ProjectName), ResourceType = typeof(Resources))]
        public string ProjectName { get; set; }

        [Display(Name = nameof(Resources.TimeTracker_Index_Hours), ResourceType = typeof(Resources))]
        [Range(0.1, 24, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.Error_Index_Hours_RangeError))]
        public decimal Hours { get; set; }

        public int ProjectId { get; set; }

        public Guid Id { get; set; }
    }
}