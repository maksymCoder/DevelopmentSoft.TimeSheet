using System;
using System.Collections.Generic;
using System.Text;
using TimeSheet.Domain.Models;

namespace TimeSheet.App.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeREpository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeREpository = employeeRepository;
        }
    }
}
