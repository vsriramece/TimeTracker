using Reviso.TimeTracker.Domain.Entities;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Reviso.TimeTracker.Infrastructure.Repository
{
    public class TimeTrackerRepository: ITimeTrackerRepository
    {
        private readonly TimeTrackerDbContext DbContext;
        public IQueryable<TimeEntry> AggregateStates => DbContext.TimeEntries.AsQueryable();
        public TimeTrackerRepository(TimeTrackerDbContext dbContext)
        {
            DbContext = dbContext; 
        }

        public TimeEntry Create(int userId, DateTime entryDate)
        {
            // For now, keeping it simple where one user can create only one timeentry for a given date
            // This can also be designed to suit multiple entries where the user can add multiple entries in different project entries
            if (AggregateStates.FirstOrDefault(o => o.UserId == userId && o.EntryDate == entryDate.Date) != null)
            {
                throw new Exception($"TimeEntry already exists for userId {userId} for date {entryDate}");
            }
           var timeEntry= new TimeEntry(userId, entryDate.Date);
           DbContext.TimeEntries.Add(timeEntry);
           return timeEntry;
        }


        public IEnumerable<TimeEntry> GetAllTimeEntries()
        {
            // To do- introduce pagination to avoid memory overflow
            return AggregateStates;
        }

        public IEnumerable<TimeEntry> GetTimeSheetEntriesForUser(int userId, DateTime? startDate, DateTime? endDate)
        {
            // To do- introduce pagination to avoid memory overflow
            var result =AggregateStates.Where(o => o.UserId == userId).ToList();
            // Optional Filters
            if (startDate.HasValue && endDate.HasValue)
            {
                return result.Where(o => o.EntryDate >= startDate.Value.Date && o.EntryDate <= endDate.Value.Date);
            }
            else if(startDate.HasValue)
            {
                return result.Where(o => o.EntryDate >= startDate.Value.Date);
            }
            else if (endDate.HasValue)
            {
                return result.Where(o => o.EntryDate <= endDate.Value.Date);
            }
            return result;
        }

        public TimeEntry GetById(Guid id)
        {
            return AggregateStates.FirstOrDefault(o => o.Id == id);
        }

        public void Delete(TimeEntry timeSheetEntry)
        {
            DbContext.TimeEntries.Remove(timeSheetEntry);
        }
    }
}
