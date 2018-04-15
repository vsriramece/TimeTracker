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

 
}