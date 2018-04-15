using Reviso.TimeTracker.UI.Infrastructure.ProxyServices;
using Reviso.TimeTracker.UI.Models;
using System;
using System.Net;
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
            model.TimeEntries = await timeTrackerDomainservice.GetRecentTimeEntries(GetCurrentLoggedInUser());
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View(new TimeEntryModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TimeEntryModel timeEntry)
        {
            if (!ModelState.IsValid)
            {
                return View(timeEntry);

            }
            await timeTrackerDomainservice.CreateTimeEntry(GetCurrentLoggedInUser(), timeEntry);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            // Edit 
            TimeEntryModel timeEntry = await timeTrackerDomainservice.GetTimeEntry(id);
            return View(timeEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TimeEntryModel timeEntry)
        {
            if (!ModelState.IsValid)
            {
                return View(timeEntry);

            }
            await timeTrackerDomainservice.UpdateTimeEntry(timeEntry);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await timeTrackerDomainservice.DeleteTimeEntry(id);
            return RedirectToAction("Index");
        }

        private int GetCurrentLoggedInUser()
        {
            // To do - Get the user id from Claims or Session
            return 123;
        }
    }
}