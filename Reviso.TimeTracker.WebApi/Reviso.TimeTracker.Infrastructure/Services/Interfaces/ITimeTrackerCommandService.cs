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
    }
}
