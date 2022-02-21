using System;
using System.IO;
using System.Text;
using TimeSheet.Domain.Models;

namespace TimeSheet.DataAccess.csv
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private const string PATH = "..\\..\\DevelopmentSoft.TimeSheet\\TimeSheet.DataAccess.csv\\Data\\Dataemployees.csv";
        private const char DELIMETER = ';';

        public void Add(StaffEmployee employee)
        {
            var dataRow = $"{employee.LastName} " +
                          $"{DELIMETER}{employee.Salary}\n";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            File.AppendAllText(PATH, dataRow, Encoding.GetEncoding(1251));
        }
        public StaffEmployee GetEmployee(string lastName)
        {
            return new StaffEmployee();
        }
    }
}
