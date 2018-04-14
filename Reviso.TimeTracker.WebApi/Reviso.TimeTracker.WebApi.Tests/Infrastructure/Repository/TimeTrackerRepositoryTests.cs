using FluentAssertions;
using Moq;
using Reviso.TimeTracker.Domain.Entities;
using Reviso.TimeTracker.Infrastructure.Repository;
using Reviso.TimeTracker.WebApi.Tests.UtilityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace Reviso.TimeTracker.WebApi.Tests.Infrastructure.Repository
{
    public class TimeTrackerRepositoryTests
    {
        [Theory, AutoMoqData]
        public void Repository_Create_Success(Mock<DbSet<TimeEntry>> dbSet,
           Mock<TimeTrackerDbContext> dbContext,
            List<TimeEntry> fakeTimeEntries,
            int userId,
            DateTime entryDate)
        {
            // Arrange
            int initialDbSetCount = fakeTimeEntries.Count();
            dbSet.SetupData(fakeTimeEntries);
            dbContext.Setup(moq => moq.TimeEntries).Returns(dbSet.Object);
            var sut = new TimeTrackerRepository(dbContext.Object);
            // Act
            var response = sut.Create(userId, entryDate);
            // Assert
            response.Id.Should().NotBeEmpty("because Id is autoset");
            response.UserId.Should().Be(userId, "because it is passed as an input");
            response.EntryDate.Should().Be(entryDate.Date, "because it is passed as an input");
            dbContext.Object.TimeEntries.Count().Should().Be(initialDbSetCount + 1, "because a new item is added to the collection");
        }

        [Theory, AutoMoqData]
        public void Repository_Create_ThrowsException_WhenAlreadyExists(Mock<DbSet<TimeEntry>> dbSet,
          Mock<TimeTrackerDbContext> dbContext,
          List<TimeEntry> fakeTimeEntries,
          int userId,
          DateTime entryDate)
        {
            // Arrange
            fakeTimeEntries.Add(new TimeEntry(userId, entryDate.Date));
            dbSet.SetupData(fakeTimeEntries);
            dbContext.Setup(moq => moq.TimeEntries).Returns(dbSet.Object);
            var sut = new TimeTrackerRepository(dbContext.Object);
            // Act
            Action act=()=> sut.Create(userId, entryDate);
            // Assert
            act.Should().Throw<Exception>("because time entry already exists").And.Message.Contains("TimeEntry already exists");
        }
    }
}
