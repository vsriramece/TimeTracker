using Reviso.TimeTracker.UI.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reviso.TimeTracker.UI.Models
{
    public class TimeEntryModel
    {
        [Display(Name = nameof(Resources.TimeTracker_Index_EntryDate), ResourceType = typeof(Resources))]
        [Required]
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }

        [StringLength(50, MinimumLength = 3)] //For now, setting the max length as 50 and Min length as 3
        [Required]
        [Display(Name = nameof(Resources.TimeTracker_Index_ProjectName), ResourceType = typeof(Resources))]
        public string ProjectName { get; set; }

        [Display(Name = nameof(Resources.TimeTracker_Index_Hours), ResourceType = typeof(Resources))]
        [Required]
        [Range(0.1, 24, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.Error_Index_Hours_RangeError))]
        public decimal Hours { get; set; }

        public int ProjectId { get; set; }

        public Guid Id { get; set; }
    }
}