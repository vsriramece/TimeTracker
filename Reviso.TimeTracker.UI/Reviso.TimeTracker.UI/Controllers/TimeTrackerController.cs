using Reviso.TimeTracker.UI.Infrastructure.ProxyServices;
using Reviso.TimeTracker.UI.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Reviso.TimeTracker.UI.Controllers
{
    public class TimeTrackerController : Controller
    {
        private readonly ITimeTrackerDomainService timeTrackerDomainservice;
        public TimeTrackerController(ITimeTrackerDomainService timeTrackerservice)
        {
            this.timeTrackerDomainservice = timeTrackerservice;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            TimeEntryIndexViewModel model = new TimeEntryIndexViewModel();
            model.TimeEntries = await timeTrackerDomainservice.GetRecentTimeEntries(GetCurrentLoggedUser());
            return View(model);
        }

        [HttpGet]
        public ActionResult TimeEntry(Guid? id)
        {
            // Create new
            if (!id.HasValue || id.Value == Guid.Empty)
            {
                return View(new TimeEntryModel());
            }
            // Edit 
            // To do - Fetch the data and send the updated model
            return View(new TimeEntryModel() { Id= id.Value});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TimeEntry(TimeEntryModel timeEntry)
        {
            if (!ModelState.IsValid)
            {
                return View(timeEntry);

            }
            // New
            if(timeEntry.Id == Guid.Empty)
            {
                await timeTrackerDomainservice.CreateTimeEntry(GetCurrentLoggedUser(), timeEntry);
            }
            else //Edit
            {
                await timeTrackerDomainservice.UpdateTimeEntry(timeEntry);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {
            await timeTrackerDomainservice.DeleteTimeEntry(id);
            return RedirectToAction("Index");
        }

        private int GetCurrentLoggedUser()
        {
            // To do - Get the user id from Claims or Session
            return 123;
        }
    }
}