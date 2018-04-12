using System;

namespace Reviso.TimeTracker.Infrastructure.DTO.Commands
{
    public class UpdateTimeEntry
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal Hours { get; set; }
    }
}
