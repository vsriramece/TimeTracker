using System;

namespace Reviso.TimeTracker.Infrastructure.DTO.Commands
{
    public class CreateTimeEntry
    {
        public int UserId {get;set;}
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal Hours { get; set; }
    }
}
