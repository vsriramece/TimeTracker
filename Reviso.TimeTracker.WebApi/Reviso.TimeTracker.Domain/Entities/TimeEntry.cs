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
        #region Behaviors
        public void Initialize(int projectId, string projectName, decimal hours)
        {
            // Domain validations
           
            if(EntryDate.Date > DateTime.UtcNow.Date) //User Time zone consideration?
            {
                throw new Exception("Entry date cannot be future date. UTC time zone is followed!");
            }
            ValidateHours(hours);
            //To do if required- project id can also be validated Eg: if the user is authorized to record against this project id

            ProjectId = projectId;
            ProjectName = projectName;
            Hours = hours;
        }

        public void Update(int projectId, string projectName, decimal hours)
        {
            ValidateHours(hours);
            ProjectId = projectId;
            ProjectName = projectName;
            Hours = hours;
        }

        public void Delete()
        {
            //No behavior as of now.
            // In future, this can be used to emit events on deletion to other systems
        }

        #endregion

        private void ValidateHours(decimal hours)
        {
            if (hours <= 0 || hours > 24)
            {
                throw new Exception("Please enter valid hours. Allowed value is between 0 and 24.");
            }
        }
    }
}
