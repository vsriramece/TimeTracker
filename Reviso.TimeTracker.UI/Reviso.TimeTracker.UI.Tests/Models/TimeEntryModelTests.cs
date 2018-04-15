using FluentAssertions;
using Reviso.TimeTracker.UI.Content;
using Reviso.TimeTracker.UI.Models;
using Reviso.TimeTracker.UI.Tests.Utilities;
using System.Linq;
using Xunit;

namespace Reviso.TimeTracker.UI.Tests.Models
{
    public class TimeEntryModelTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(0.01)]
        [InlineData(25)]
        public void TimeEntryModel_Invalid_Hours(int hours)
        {
            // Arrange
            var model = new TimeEntryModel();
            model.Hours = hours;
            // Act
            var results = ModelValidator.Validate(model);
            // Assert
            results.Count(o=>o.ErrorMessage == Resources.Error_Index_Hours_RangeError).Should().Be(1, "because hours field in the model is invalid");
        }
    }
}
