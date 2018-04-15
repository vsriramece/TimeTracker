using System;

namespace Reviso.TimeTracker.UI.Infrastructure.Dto
{
    public class TimeEntryData
    {
        public Guid Id { get; set; }
        public DateTime EntryDate { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal Hours { get; set; }
    }
}