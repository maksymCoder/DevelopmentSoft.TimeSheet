

namespace TimeSheet.Domain.Models
{
    public interface IEmployeeReportService
    {
        public EmployeeReport GetEmployeeReport(string lastName);
    }
}
