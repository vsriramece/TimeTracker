using Reviso.TimeTracker.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Reviso.TimeTracker.Infrastructure.Repository
{
    public interface ITimeTrackerRepository
    {
        TimeEntry Create(int userId, DateTime entryDate);
        IEnumerable<TimeEntry> GetAllTimeEntries();
        IEnumerable<TimeEntry> GetTimeSheetEntriesForUser(int userId, DateTime? startDate, DateTime? endDate);
        TimeEntry GetById(Guid id);
        void Delete(TimeEntry timeSheetEntry);
    }
}
