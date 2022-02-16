using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Domain.Models;
using TimeSheet.App.Services;
using Moq;

namespace TimeSheet.Tests
{
    class TimeSheetServiceTest
    {
        [Test]
        public void TrackTime_ShouldReturnTrue()
        {
            //arrange
            var expectedLastName = "TestUser";

            UserSession.Sessions.Add(expectedLastName);

            var timeLog = new TimeLog()
            {
                Date = new DateTime(),
                WorkingHours = 1,
                LastName = expectedLastName,
                Comment = Guid.NewGuid().ToString()
            };

            var timeSheetRepositoryMock = new Mock<ITimeSheetRepository>();
            timeSheetRepositoryMock
                .Setup(x => x.Add(timeLog))
                .Verifiable();

            var service = new TimeSheetService(timeSheetRepositoryMock.Object);
            //act

            var result = service.TrackTime(timeLog);

            //assert
            timeSheetRepositoryMock.Verify(x => x.Add(timeLog), Times.Once());
            Assert.IsTrue(result);
        }
        [TestCase(25, "")]
        [TestCase(25, null)]
        [TestCase(25, "TestUser")]
        [TestCase(-1, "")]
        [TestCase(-1, null)]
        [TestCase(-1, "TestUser")]
        [TestCase(5, "")]
        [TestCase(5, null)]
        [TestCase(5, "TestUser")]
        public void TrackTime_ShouldReturnFalse(int hours, string lastName)
        {
            //arrange

            var timeLog = new TimeLog()
            {
                Date = new DateTime(),
                WorkingHours = hours,
                LastName = lastName,
                Comment = Guid.NewGuid().ToString()
            };
            var timeSheetRepositoryMock = new Mock<ITimeSheetRepository>();
            timeSheetRepositoryMock
                .Setup(x => x.Add(timeLog))
                .Verifiable();
            var service = new TimeSheetService(timeSheetRepositoryMock.Object);
            //act
            var result = service.TrackTime(timeLog);

            //assert
            timeSheetRepositoryMock.Verify(x => x.Add(timeLog), Times.Never());
            Assert.IsFalse(result);
        }
    }
}
