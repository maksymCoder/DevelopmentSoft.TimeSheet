using System;
using TimeSheet.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace TimeSheet.App.Services
{
    public class ReportService:IEmployeeReportService
    {
        private const decimal MAX_WORKING_HOURS_PER_MONTH = 160;
        private const decimal MAX_WORKING_HOURS_PER_DAY = 8;

        private readonly ITimeSheetRepository _timeSheetRepository;
        private readonly IEmployeeRepository _employeeRepository;
        
        public ReportService(ITimeSheetRepository timeSheetRepository, IEmployeeRepository employeeRepository)
        {
            _timeSheetRepository = timeSheetRepository;
            _employeeRepository = employeeRepository;
        }
        public EmployeeReport GetEmployeeReport(string lastName)
        {
            var employee = _employeeRepository.GetEmployee(lastName);
            var timeLogs = _timeSheetRepository.GetTimeLogs(employee.LastName);
            var totalHours = timeLogs.Sum(x => x.WorkingHours);

            var bill = 0m;
            int[] hours = new int[12];

            if(  timeLogs == null || timeLogs.Length == 0)
            {
                return new EmployeeReport
                {
                    Bill = 0,
                    TimeLogs = new List<TimeLog>(),
                    TotalHours = totalHours,
                    LastName = employee.LastName
                };
            }

            var timeLogByMonths = timeLogs.GroupBy(x => x.Date.Month);

            foreach(var timeLogByMonth in timeLogByMonths)
            {
                var workingHours = 0;

                foreach (var timeLog in timeLogByMonth)
                {
                    workingHours = timeLog.WorkingHours;

                    if(workingHours > MAX_WORKING_HOURS_PER_DAY)
                    {
                        var overtime = workingHours - MAX_WORKING_HOURS_PER_DAY;
                        bill += (overtime / MAX_WORKING_HOURS_PER_MONTH) * (employee.Salary * 2);
                        bill += (MAX_WORKING_HOURS_PER_DAY / MAX_WORKING_HOURS_PER_MONTH) * employee.Salary;
                    }
                    else
                    {
                        bill += (MAX_WORKING_HOURS_PER_DAY / MAX_WORKING_HOURS_PER_MONTH) * employee.Salary;
                    }
                }
            }
            return new EmployeeReport
            {
                LastName = employee.LastName,
                TimeLogs = timeLogs.ToList(),
                Bill = bill,
                TotalHours = totalHours,
            };
        }
        
    }
}