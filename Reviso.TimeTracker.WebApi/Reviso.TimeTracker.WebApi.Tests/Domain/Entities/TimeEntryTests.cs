using AutoFixture.Xunit2;
using FluentAssertions;
using Reviso.TimeTracker.Domain.Entities;
using System;
using Xunit;

namespace Reviso.TimeTracker.WebApi.Tests.Domain.Entities
{
    public class TimeEntryTests
    {
        [Theory]
        [InlineAutoData(-1)]
        [InlineAutoData(0)]
        [InlineAutoData(25)]
        public void TimeEntry_Initialize_HoursValidation(int hours)
        {
            // Arrange
            int projectId = 1;
            string projectName = "Testproj";
            TimeEntry sut = new TimeEntry(1, DateTime.UtcNow);
            // Act
            Action act = ()=>sut.Initialize(projectId, projectName, hours);
            // Assert
            act.Should().Throw<Exception>($"because hours:{hours} is invalid").And.Message.Contains("Please enter valid hours. Allowed value is between 0 and 24.");
        }

        [Theory]
        [InlineAutoData("   ")]
        [InlineAutoData(null)]
        [InlineAutoData("")]
        public void TimeEntry_Initialize_ProjectNameValidation(string projectName)
        {
            // Arrange
            int projectId = 1;
            decimal hours = 2;
            TimeEntry sut = new TimeEntry(1, DateTime.UtcNow);
            // Act
            Action act = () => sut.Initialize(projectId, projectName, hours);
            // Assert
            act.Should().Throw<Exception>($"because project name:{projectName} is invalid").And.Message.Contains("Please enter a valid project name.");
        }

        [Theory, AutoData]
        public void TimeEntry_Initialize_ThrowsException_FutureDate(int projectId, string projectName, int hours)
        {
            // Arrange
            TimeEntry sut = new TimeEntry(1, DateTime.UtcNow.AddDays(1));
            Action act = () => sut.Initialize(projectId, projectName, hours);
            // Assert
            act.Should().Throw<Exception>($"because entry date: {sut.EntryDate} is invalid").And.Message.Contains("Entry date cannot be future date");
        }

        [Fact]
        public void TimeEntry_Initialize_Success()
        {
            // Arrange
            int projectId = 1;
            string projectName = "Testproj";
            decimal hours = 4;
            TimeEntry sut = new TimeEntry(1, DateTime.UtcNow);
            // Act
            sut.Initialize(projectId, projectName, hours);
            // Assert
            sut.Hours.Should().Be(hours, "because the hours value should be set in the domain entity");
            sut.ProjectName.Should().Be(projectName, "because the projectName value should be set in the domain entity");
            sut.ProjectId.Should().Be(projectId, "because the projectName value should be set in the domain entity");
        }

    }
}
