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
            var expectedTotal = 211_500m; 
            var expectedTotalHourse = 281; 

            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock
                .Setup(x => x.GetEmployee(It.Is<string>(y => y == expectedLastName)))
                .Returns(() =>
                    new StaffEmployee
                    {
                        LastName = expectedLastName,
                        Salary = 120000m
                    })
                .Verifiable();

            timeSheetRepositoryMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() =>
                {
                    TimeLog[] timeLogs = new TimeLog[35];
                    DateTime date = new DateTime(2021, 11, 1);

                    timeLogs[0] = new TimeLog
                    {
                        LastName = expectedLastName,
                        Date = date,
                        WorkingHours = 9,
                        Comment = Guid.NewGuid().ToString()
                    };
                    for (int i = 1; i < 35; i++)
                    {
                        date.AddDays(1);
                        timeLogs[i] = new TimeLog
                        {
                            Date = date,
                            WorkingHours = 8,
                            Comment = Guid.NewGuid().ToString()
                        };
                    }
                    
                    return timeLogs;
                })
                .Verifiable();

            var service = new ReportService(timeSheetRepositoryMock.Object, employeeRepositoryMock.Object);

            //act

            var result = service.GetEmployeeReport("Иванов");


            //assert

            timeSheetRepositoryMock.VerifyAll();
            employeeRepositoryMock.VerifyAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);
            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);
            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHourse, result.TotalHours);

        }

        [Test]
        public void ReportService_WithoutTimeLogs_ShouldReturnReport()
        {
            //arrange
            var timeSheetRepositoryMock = new Mock<ITimeSheetRepository>();
            var expectedLastName = "Иванов";
            var expectedTotal = 0m;
            var expectedTotalHourse = 0;

            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock
                .Setup(x => x.GetEmployee(It.Is<string>(y => y == expectedLastName)))
                .Returns(() =>
                    new StaffEmployee
                    {
                        LastName = expectedLastName,
                        Salary = 120000m
                    })
                .Verifiable();

            timeSheetRepositoryMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new TimeLog[0])
                .Verifiable();


            var service = new ReportService(timeSheetRepositoryMock.Object, employeeRepositoryMock.Object);

            //act

            var result = service.GetEmployeeReport(expectedLastName);


            //assert

            timeSheetRepositoryMock.VerifyAll();
            employeeRepositoryMock.VerifyAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);
            Assert.IsNotNull(result.TimeLogs);
            Assert.IsEmpty(result.TimeLogs);
            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHourse, result.TotalHours);

        }
        [Test]
        public void ReportService_TimeLogsOneDay_ShouldReturnReport()
        {
            //arrange
            var timeSheetRepositoryMock = new Mock<ITimeSheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();

            var expectedLastName = "Иванов";
            var expectedTotal = 6000m;  //8/160*120_000
            var expectedTotalHourse = 8;


            employeeRepositoryMock
                .Setup(x => x.GetEmployee(It.Is<string>(y => y == expectedLastName)))
                .Returns(() =>
                    new StaffEmployee
                    {
                        LastName = expectedLastName,
                        Salary = 120000m
                    })
                .Verifiable();

            timeSheetRepositoryMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new[]
                {
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Date =  DateTime.Now.AddDays(-1),
                        WorkingHours = 8,
                        Comment =  Guid.NewGuid().ToString()
                    }
                })
                .Verifiable();


            var service = new ReportService(timeSheetRepositoryMock.Object, employeeRepositoryMock.Object);

            //act

            var result = service.GetEmployeeReport(expectedLastName);


            //assert

            timeSheetRepositoryMock.VerifyAll();
            employeeRepositoryMock.VerifyAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);
            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);
            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHourse, result.TotalHours);

        }
    }
}
