using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reviso.TimeTracker.Infrastructure.DTO.Commands;
using Reviso.TimeTracker.Infrastructure.DTO.Response;

namespace Reviso.TimeTracker.Infrastructure.Services.Interfaces
{
    public interface ITimeTrackerCommandService
    {
        Task<CreateTimeEntryResponse> CreateTimeSheetEntry(CreateTimeEntry input);
        Task<UpdateTimeEntryResponse> UpdateTimeSheetEntry(Guid id, UpdateTimeEntry input);
        Task<DeleteTimeEntryResponse> DeleteTimeSheetEntry(Guid id);
    }
}
