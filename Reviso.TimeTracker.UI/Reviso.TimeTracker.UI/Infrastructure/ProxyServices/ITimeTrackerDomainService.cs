using Reviso.TimeTracker.UI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reviso.TimeTracker.UI.Infrastructure.ProxyServices
{
    public interface ITimeTrackerDomainService
    {
        Task<IEnumerable<TimeEntryModel>> GetRecentTimeEntries(int userId);
        Task<Guid> CreateTimeEntry(int userId, TimeEntryModel data);
        Task<bool> UpdateTimeEntry(TimeEntryModel data);
        Task<bool> DeleteTimeEntry(Guid id);
    }
}
