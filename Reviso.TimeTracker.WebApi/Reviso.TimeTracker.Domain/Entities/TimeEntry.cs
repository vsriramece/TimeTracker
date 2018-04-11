using System;

namespace Reviso.TimeTracker.Domain.Entities
{
    public class TimeEntry
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal Hours { get; set; }
        public TimeEntry()
        {
        }

        public TimeEntry(int userId, DateTime entryDate)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            EntryDate = entryDate;
        }

        public void Initialize(int projectId, string projectName, decimal hours)
        {
            // Domain validations
            if(hours < 0 || hours > 24)
            {
                throw new Exception("Please enter valid hours. Allowed value is between 0 and 24.");
            }

            //To do if required- project id can also be validated Eg: if the user is authorized to record against this project id

            ProjectId = projectId;
            ProjectName = projectName;
            Hours = hours;
        }
        #region Behaviors
        #endregion
    }
}
