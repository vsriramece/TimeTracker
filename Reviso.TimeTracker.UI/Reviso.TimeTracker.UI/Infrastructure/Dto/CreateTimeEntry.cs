using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reviso.TimeTracker.UI.Infrastructure.Dto
{
    public class CreateTimeEntry
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal Hours { get; set; }
    }
}