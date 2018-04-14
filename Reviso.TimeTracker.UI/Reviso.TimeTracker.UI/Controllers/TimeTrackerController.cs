using Reviso.TimeTracker.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Reviso.TimeTracker.UI.Controllers
{
    public class TimeTrackerController : Controller
    {

        // GET: TimeTracker
        public ActionResult Index()
        {
            TimeEntryIndexViewModel model = new TimeEntryIndexViewModel();
            List<TimeEntryModel> items = new List<TimeEntryModel>();
            items.Add(new TimeEntryModel() { Id = Guid.NewGuid(), EntryDate = DateTime.Now, Hours = 2, ProjectName = "Test1", ProjectId = 1 });
            items.Add(new TimeEntryModel() { Id = Guid.NewGuid(), EntryDate = DateTime.Now, Hours = 2, ProjectName = "Test2", ProjectId = 1 });
            items.Add(new TimeEntryModel() { Id = Guid.NewGuid(), EntryDate = DateTime.Now, Hours = 2, ProjectName = "Test3", ProjectId = 1 });
            items.Add(new TimeEntryModel() { Id = Guid.NewGuid(), EntryDate = DateTime.Now, Hours = 2, ProjectName = "Test4", ProjectId = 1 });
            model.TimeEntries = items;
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
        public ActionResult TimeEntry(TimeEntryModel timeEntry)
        {
            if (!ModelState.IsValid)
            {
                return View(timeEntry);

            }
            return RedirectToAction("Index");
        }
    }
}