namespace Reviso.TimeTracker.UI.Infrastructure.Dto
{
    public class UpdateTimeEntry
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal Hours { get; set; }
    }
}