using FluentAssertions;
using Moq;
using Reviso.TimeTracker.UI.Controllers;
using Reviso.TimeTracker.UI.Infrastructure.ProxyServices;
using Reviso.TimeTracker.UI.Models;
using Reviso.TimeTracker.UI.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Reviso.TimeTracker.UI.Tests.Controllers
{
    public class TimeTrackerControllerTests
    {
        [Theory, AutoMoqData]
        public void TimeTrackerController_GET_Index_Success(Mock<ITimeTrackerDomainService> domainService,
            IEnumerable<TimeEntryModel> timeEntries)
        {
            // Arrange
            int userId = 123; //This is hardcoded now
            domainService.Setup(o => o.GetRecentTimeEntries(userId)).Returns(Task.FromResult(timeEntries));
            var sut = new TimeTrackerController(domainService.Object);

            // Act
            var result = sut.Index().Result as ViewResult;

            // Assert
            var actualModel = result.Model as TimeEntryIndexViewModel;
            actualModel.TimeEntries.Should().BeEquivalentTo(timeEntries,"because the collection is set as a mock param");
            result?.ViewName.Should().Be("Index","because the viewname should match the action");
            domainService.Verify(o => o.GetRecentTimeEntries(userId),"because the domainservice is called in the action");
        }

        [Theory, AutoMoqData]
        public void TimeTrackerController_POST_Create_ModelError(Mock<ITimeTrackerDomainService> domainService, TimeEntryModel model)
        {
            // Arrange
            var sut = new TimeTrackerController(domainService.Object);
            sut.ModelState.AddModelError("", "Mock error");
            // Act
            var result = sut.Create(model).Result as ViewResult;

            // Assert
            result?.ViewName.Should().Be(nameof(sut.Create), "because the viewname should match the GET action as the model state has error");
        }

        [Theory, AutoMoqData]
        public void TimeTrackerController_POST_Create_Success(Mock<ITimeTrackerDomainService> domainService, 
            TimeEntryModel model)
        {
            // Arrange
            var sut = new TimeTrackerController(domainService.Object);
            domainService.Setup(o => o.CreateTimeEntry(123, model)).Returns(Task.FromResult(Guid.NewGuid())); //UserId hardcoded
            // Act
            var result = sut.Create(model).Result as RedirectToRouteResult;

            // Assert
            result.RouteValues["action"].Should().Be(nameof(sut.Index), "because after successful creation, redirection happens to Index");
        }
    }
}
