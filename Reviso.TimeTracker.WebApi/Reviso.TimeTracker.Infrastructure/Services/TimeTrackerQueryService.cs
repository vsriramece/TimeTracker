using Reviso.TimeTracker.Infrastructure.Repository;
using Reviso.TimeTracker.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reviso.TimeTracker.Infrastructure.DTO;
using Reviso.TimeTracker.Domain.Entities;

namespace Reviso.TimeTracker.Infrastructure.Services
{
    public class TimeTrackerQueryService: ITimeTrackerQueryService
    {
        private readonly ITimeTrackerRepository Repository;
        public TimeTrackerQueryService(ITimeTrackerRepository repository)
        {
            Repository = repository;
        }

        public Task<List<TimeEntryData>> GetTimeSheetEntries()
        {
            IEnumerable<TimeEntry> timeEntries = Repository.GetAllTimeEntries();
            List<TimeEntryData> result = new List<TimeEntryData>();
            result = timeEntries.Select(timeEntry => MapTimeEntryDomainFields(timeEntry)).ToList();
            return Task.FromResult(result);
        }

        public Task<List<TimeEntryData>> GetTimeSheetEntriesForUser(int userId, DateTime? startDate, DateTime? endDate)
        {
            IEnumerable<TimeEntry> timeEntries = Repository.GetTimeSheetEntriesForUser(userId, startDate, endDate);
            List<TimeEntryData> result = new List<TimeEntryData>();
            result = timeEntries.Select(timeEntry => MapTimeEntryDomainFields(timeEntry)).ToList();
            return Task.FromResult(result);
        }

        private TimeEntryData MapTimeEntryDomainFields(TimeEntry entry)
        {
            // To do - Possibly use Automapper instead of custom translating fields
            return new TimeEntryData()
            {
                Id = entry.Id,
                EntryDate = entry.EntryDate,
                Hours = entry.Hours,
                ProjectId = entry.ProjectId,
                ProjectName = entry.ProjectName,
                UserId = entry.UserId
            };
        }
    }
}
