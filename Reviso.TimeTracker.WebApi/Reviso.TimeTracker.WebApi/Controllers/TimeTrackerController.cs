using Reviso.TimeTracker.Infrastructure.DTO;
using Reviso.TimeTracker.Infrastructure.DTO.Commands;
using Reviso.TimeTracker.Infrastructure.DTO.Response;
using Reviso.TimeTracker.Infrastructure.Services.Interfaces;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Reviso.TimeTracker.WebApi.Controllers
{
    public class TimeTrackerController : ApiController
    {
        private readonly ITimeTrackerQueryService QueryService;
        private readonly ITimeTrackerCommandService CommandService;
        public TimeTrackerController(ITimeTrackerQueryService queryService,
            ITimeTrackerCommandService commandService)
        {
            QueryService = queryService;
            CommandService = commandService;
        }

        #region Command Actions
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CreateTimeEntryResponse))]
        [HttpPost, Route("timeentries")]
        public async Task<IHttpActionResult> CreateTimeSheetEntry([FromBody]CreateTimeEntry input)
        {
            try
            {
                return Ok(await CommandService.CreateTimeSheetEntry(input));
            }
            catch (Exception ex)
            {
                // To do -Logging
                // Can be fine tuned to throw a general exception instead of sending the server exception
                return InternalServerError(ex);
            }
        }

        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UpdateTimeEntryResponse))]
        [HttpPut, Route("timeentries/{id}")]
        public async Task<IHttpActionResult> UpdateTimeSheetEntry([FromUri]Guid id,[FromBody]UpdateTimeEntry input)
        {
            try
            {
                return Ok(await CommandService.UpdateTimeSheetEntry(id,input));
            }
            catch (Exception ex)
            {
                // To do -Logging
                // Can be fine tuned to throw a general exception instead of sending the server exception
                return InternalServerError(ex);
            }
        }

        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UpdateTimeEntryResponse))]
        [HttpDelete, Route("timeentries/{id}")]
        public async Task<IHttpActionResult> DeleteTimeSheetEntry([FromUri]Guid id)
        {
            try
            {
                return Ok(await CommandService.DeleteTimeSheetEntry(id));
            }
            catch (Exception ex)
            {
                // To do -Logging
                // Can be fine tuned to throw a general exception instead of sending the server exception
                return InternalServerError(ex);
            }
        }

        #endregion

        #region Queries
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<TimeEntryData>),Description = "This method needs pagination!")]
        [HttpGet, Route("timeentries")]
        [Obsolete("This method needs pagination!")]
        public async Task<IHttpActionResult> GetTimeSheetEntries()
        {
            try
            {
                // To do- introduce pagination to avoid memory overflow
                return Ok(await QueryService.GetTimeSheetEntries());
            }
            catch (Exception ex)
            {
                // To do -Logging
                // Can be fine tuned to throw a general exception instead of sending the server exception
                return InternalServerError(ex);
            }
        }

        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(TimeEntryData))]
        [HttpGet, Route("timeentries/{id}")]
        public async Task<IHttpActionResult> GetTimeSheetEntry(Guid id)
        {
            try
            {
                var timeSheetEntry =await QueryService.GetTimeSheetEntry(id);
                if(timeSheetEntry == null)
                {
                    return NotFound();
                }
                return Ok(timeSheetEntry);
            }
            catch (Exception ex)
            {
                // To do -Logging
                // Can be fine tuned to throw a general exception instead of sending the server exception
                return InternalServerError(ex);
            }
        }

        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<TimeEntryData>))]
        [HttpGet, Route("timeentries/users/{userId}")]
        public async Task<IHttpActionResult> GetTimeSheetEntriesForUser([FromUri]int userId, [FromUri]DateTime? startDate=null, [FromUri]DateTime? endDate=null)
        {
            try
            {
                // OData querying is also an option
                // To do- introduce pagination to avoid memory overflow
                return Ok(await QueryService.GetTimeSheetEntriesForUser(userId, startDate, endDate));
            }
            catch (Exception ex)
            {
                // To do -Logging
                // Can be fine tuned to throw a general exception instead of sending the server exception
                return InternalServerError(ex);
            }
        }
        #endregion
    }
}
