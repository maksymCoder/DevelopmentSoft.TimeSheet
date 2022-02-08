namespace TimeShieet.Domain.Models
{
    public interface IEmployeeRepository
    {
        StaffEmployee GetEmployee(string lastName);
    }
}