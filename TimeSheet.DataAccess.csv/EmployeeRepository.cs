using System;
using System.Collections.Generic;
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
            var data = File.ReadAllText(PATH);
            var dataMembers = data.Split('\n');
            StaffEmployee staffEmployee = null;

            foreach (var dataMember in dataMembers)
            {
                if (dataMember.Contains(lastName){
                    var fields = dataMember.Split(DELIMETER);
                    staffEmployee = new StaffEmployee
                    {
                        LastName = fields[0],
                        Salary = decimal.TryParse(fields[1], out var salary) ? salary: 0.0m  
                    };
                }

            }
            return staffEmployee;
        }
    }
}
