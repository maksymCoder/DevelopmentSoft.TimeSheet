using System;
using TimeSheet.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using TimeShieet.Domain.Models;

namespace TimeSheet.App.Services
{
    public class ReportService
    {
        private const decimal MAX_WORKING_HOURS_PER_MONTH = 160m;

        private readonly ITimeSheetRepository _timeSheetRepository;
        private readonly IEmployeeRepository _employeeRepository;
        
        public ReportService(ITimeSheetRepository timeSheetRepository, IEmployeeRepository employeeRepository)
        {
            _timeSheetRepository = timeSheetRepository;
            _employeeRepository = employeeRepository;
        }
        public EmployeeReport GetEmployeeRepeort(string lastName)
        {
            var employee = _employeeRepository.GetEmployee(lastName);
            var timeLogs = _timeSheetRepository.GetTimeLogs(employee.LastName);
            
            var hours = 0;

            foreach (var timeLog in timeLogs)
            {
                hours += timeLog.WorkingHours;
            }

            var bill = (hours / MAX_WORKING_HOURS_PER_MONTH) * employee.Salary;
            
            return new EmployeeReport
            {
                LastName = employee.LastName,
                TimeLogs = timeLogs.ToList(),
                Bill = bill,
            };
        }
    }
}