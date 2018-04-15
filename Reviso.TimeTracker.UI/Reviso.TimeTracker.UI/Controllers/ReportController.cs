using Reviso.TimeTracker.UI.Infrastructure.ProxyServices;
using Reviso.TimeTracker.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Reviso.TimeTracker.UI.Controllers
{
    public class ReportController : Controller
    {
        private readonly ITimeTrackerDomainService timeTrackerDomainservice;
        public ReportController(ITimeTrackerDomainService timeTrackerservice)
        {
            this.timeTrackerDomainservice = timeTrackerservice;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(new ReportFilter());
        }

        [HttpPost]
        public async Task<ActionResult> Summary(ReportFilter filter)
        {
            ReportSummary summary = new ReportSummary();
            summary.StartDate = filter.StartDate.Date;
            summary.EndDate = filter.EndDate.Date;
            summary.TimeEntries = (await timeTrackerDomainservice.GetTimeEntriesForRange(GetCurrentLoggedInUser(), filter.StartDate.Date, filter.EndDate.Date)).ToList();
            summary.ProjectSummary = summary.TimeEntries.GroupBy(o=> o.ProjectName)
                                    .Select(item => new ProjectSummary
                                    {
                                        ProjectName = item.First().ProjectName,
                                        TotalHours = item.Sum(c => c.Hours)
                                    }).ToList();
            return View(summary);
        }

        private int GetCurrentLoggedInUser()
        {
            // To do - Get the user id from Claims or Session
            return 123;
        }
    }
}