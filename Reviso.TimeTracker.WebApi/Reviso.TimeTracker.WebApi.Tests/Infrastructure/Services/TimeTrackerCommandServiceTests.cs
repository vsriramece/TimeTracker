using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Reviso.TimeTracker.Domain.Entities;
using Reviso.TimeTracker.Infrastructure.DTO.Commands;
using Reviso.TimeTracker.Infrastructure.Repository;
using Reviso.TimeTracker.Infrastructure.Services;
using Reviso.TimeTracker.WebApi.Tests.UtilityFramework;
using System;
using Xunit;

namespace Reviso.TimeTracker.WebApi.Tests.Infrastructure.Services
{
    public class TimeTrackerCommandServiceTests
    {
        [Theory, AutoMoqData]
        public async void UpdateTimeSheetEntry_Success([Frozen]Mock<ITimeTrackerRepository> repository,
            Guid id, 
            UpdateTimeEntry input,
            TimeEntry entry,
            TimeTrackerCommandService sut)
        {
            // Arrange
            repository.Setup(o => o.GetById(id)).Returns(entry);
            input.Hours = 2; // Valid value as fixture might set an invalid hours value - This can also be handled by using Fixture specimen builder
            // Act
            var response =await sut.UpdateTimeSheetEntry(id, input);
            //Assert
            response.Success.Should().BeTrue("because the action is successful");
            entry.Hours.Should().Be(input.Hours, "because the timesheet entry is updated");
            entry.ProjectId.Should().Be(input.ProjectId, "because the timesheet entry is updated");
            entry.ProjectName.Should().Be(input.ProjectName, "because the timesheet entry is updated");
        }

        [Theory, AutoMoqData]
        public void UpdateTimeSheetEntry_InvalidId([Frozen]Mock<ITimeTrackerRepository> repository,
           Guid id,
           UpdateTimeEntry input,
           TimeTrackerCommandService sut)
        {
            // Arrange
            repository.Setup(o => o.GetById(id)).Returns<TimeEntry>(null);
            // Act
            Action act = () =>
            {
                var response = sut.UpdateTimeSheetEntry(id, input).GetAwaiter().GetResult();
            };
            //Assert
            act.Should().Throw<Exception>("because the id is invalid").And.Message.Contains($"User:{id} not found");
        }
    }
}
