using Reviso.TimeTracker.Infrastructure.Repository;
using Reviso.TimeTracker.Infrastructure.Services.Interfaces;
using System.Threading.Tasks;
using Reviso.TimeTracker.Infrastructure.DTO.Commands;
using Reviso.TimeTracker.Infrastructure.DTO.Response;
using Reviso.TimeTracker.Infrastructure.UnitOfWork;

namespace Reviso.TimeTracker.Infrastructure.Services
{
    public class TimeTrackerCommandService:ITimeTrackerCommandService
    {
        private readonly ITimeTrackerRepository Repository;
        private readonly IUnitOfWork UnitOfWork;
        public TimeTrackerCommandService(ITimeTrackerRepository repository, IUnitOfWork unitofWork)
        {
            Repository = repository;
            UnitOfWork = unitofWork;
        }

        public async Task<CreateTimeEntryResponse> CreateTimeSheetEntry(CreateTimeEntry input)
        {
            var timeSheetEntry = Repository.Create(input.UserId, input.EntryDate);
            timeSheetEntry.Initialize(input.ProjectId, input.ProjectName, input.Hours);
            await UnitOfWork.Commit();
            return new CreateTimeEntryResponse { TimeEntryId = timeSheetEntry.Id };
        }
    }
}
