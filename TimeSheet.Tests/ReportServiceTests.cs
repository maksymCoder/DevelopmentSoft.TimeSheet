using Moq;
using NUnit.Framework;
using TimeSheet.App.Services;
using TimeSheet.Domain.Models;
using System;
using TimeShieet.Domain.Models;

namespace TimeSheet.Tests
{
    class ReportServiceTests
    {
        [Test]
        public void ReportService_ShouldReturnReport()
        {
            //arrange
            var timeSheetRepositoryMock = new Mock<ITimeSheetRepository>();
            var expectedLastName = "Иванов";
            var expectedTotal = 15000m;

            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock
                .Setup(x => x.GetEmployee(It.Is<string>(y => y == expectedLastName)))
                .Returns(() =>
                    new StaffEmployee
                    {
                        LastName = expectedLastName,
                        Salary = 120000
                    })
                .Verifiable();
                    
            timeSheetRepositoryMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new[]
                {
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Date = DateTime.Now.AddDays(-2),
                        WorkingHours = 8,
                        Comment = Guid.NewGuid().ToString()
                    },
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Date = DateTime.Now.AddDays(-1),
                        WorkingHours = 8,
                        Comment = Guid.NewGuid().ToString()
                    },
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Date = DateTime.Now,
                        WorkingHours = 4,
                        Comment = Guid.NewGuid().ToString()
                    }
                })
                .Verifiable();

            var service = new ReportService(timeSheetRepositoryMock.Object, employeeRepositoryMock.Object);

            //act

            var result =  service.GetEmployeeRepeort("Иванов");


            //assert

            timeSheetRepositoryMock.VerifyAll();
            employeeRepositoryMock.VerifyAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);
            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);
            Assert.AreEqual(expectedTotal, result.Bill);
            
        }
    }
}
