using TimeSheet.Domain.Models;

namespace TimeShieet.Domain.Models
{
    public interface ITimeSheetRepository
    {
        TimeLog[] GetTimeLogs(string lastName);
    }
}
