using System.Collections.Generic;
using System.Linq;
using Reviso.TimeTracker.UI.Models;
using System.Threading.Tasks;
using Reviso.Common.HttpAccess;
using Reviso.TimeTracker.UI.Infrastructure.Dto;
using System;

namespace Reviso.TimeTracker.UI.Infrastructure.ProxyServices
{
    public class TimeTrackerDomainService : ITimeTrackerDomainService
    {
        private readonly IApiClient timeTrackerApiClient;
        public TimeTrackerDomainService(IApiClient timeTrackerClient)
        {
            timeTrackerApiClient = timeTrackerClient;
        }
        public async Task<IEnumerable<TimeEntryModel>> GetRecentTimeEntries(int userId)
        {
            // To do -Pagination/Batch count 
            var timeEntries = await timeTrackerApiClient.GetAsync<IEnumerable<TimeEntryData>>($"/timeentries/users/{userId}");
            return timeEntries.Select(o => MapTimeEntryDtoToModel(o)).OrderByDescending(o=>o.EntryDate);
       }

        public async Task<Guid> CreateTimeEntry(int userId, TimeEntryModel data)
        {
            CreateTimeEntry request = new Dto.CreateTimeEntry()
            {
                UserId = userId,
                EntryDate = data.EntryDate,
                Hours = data.Hours,
                ProjectId = data.ProjectId,
                ProjectName = data.ProjectName
            };
            var response =await timeTrackerApiClient.PostAsync<CreateTimeEntry, CreateTimeEntryResponse>($"/timeentries", request);
            return response.TimeEntryId;
        }

        public async Task<bool> UpdateTimeEntry(TimeEntryModel data)
        {
            UpdateTimeEntry request = new UpdateTimeEntry()
            { Hours = data.Hours, ProjectId = data.ProjectId, ProjectName = data.ProjectName };

            var response = await timeTrackerApiClient.PutAsync<UpdateTimeEntry, UpdateTimeEntryResponse>($"/timeentries", request);
            return response.Success;
        }

        public async Task<bool> DeleteTimeEntry(Guid id)
        {
            var response = await timeTrackerApiClient.DeleteAsync<DeleteTimeEntryResponse>($"/timeentries/{id}");
            return response.Success;
        }

        private TimeEntryModel MapTimeEntryDtoToModel(TimeEntryData data)
        {
            // To do - can use Automapper
            return new TimeEntryModel()
            {
                Id = data.Id,
                EntryDate = data.EntryDate,
                Hours = data.Hours,
                ProjectId = data.ProjectId,
                ProjectName = data.ProjectName
            };
        }
    }
}