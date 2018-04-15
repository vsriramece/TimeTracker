using Reviso.TimeTracker.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reviso.TimeTracker.Infrastructure.Services.Interfaces
{
    public interface ITimeTrackerQueryService
    {
        Task<List<TimeEntryData>> GetTimeSheetEntries();
        Task<List<TimeEntryData>> GetTimeSheetEntriesForUser(int userId, DateTime? startDate, DateTime? endDate);
        Task<TimeEntryData> GetTimeSheetEntry(Guid id);
    }
}
