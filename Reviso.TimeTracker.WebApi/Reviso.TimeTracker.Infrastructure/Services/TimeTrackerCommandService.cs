using Reviso.TimeTracker.Infrastructure.Repository;
using Reviso.TimeTracker.Infrastructure.Services.Interfaces;
using System.Threading.Tasks;
using Reviso.TimeTracker.Infrastructure.DTO.Commands;
using Reviso.TimeTracker.Infrastructure.DTO.Response;
using Reviso.TimeTracker.Infrastructure.UnitOfWork;
using System;

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

        public async Task<DeleteTimeEntryResponse> DeleteTimeSheetEntry(Guid id)
        {
            var timeSheetEntry = Repository.GetById(id);
            if (timeSheetEntry == null)
            {
                // This can be a custom exception so that NotFound (404) could be thrown
                throw new Exception($"User:{id} not found");
            };
            timeSheetEntry.Delete();
            Repository.Delete(timeSheetEntry);
            await UnitOfWork.Commit();
            return new DeleteTimeEntryResponse { Success = true };
        }

        public async Task<UpdateTimeEntryResponse> UpdateTimeSheetEntry(Guid id, UpdateTimeEntry input)
        {
            var timeSheetEntry = Repository.GetById(id);
            if (timeSheetEntry == null)
            {
                // This can be a custom exception so that NotFound (404) could be thrown
                throw new Exception($"User:{id} not found");
            };
            timeSheetEntry.Update(input.ProjectId, input.ProjectName, input.Hours);
            await UnitOfWork.Commit();
            return new UpdateTimeEntryResponse { Success = true};
        }
    }
}
