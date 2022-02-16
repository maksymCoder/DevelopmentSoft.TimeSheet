

namespace TimeSheet.Domain.Models
{
    public interface ITimeSheetRepository
    {
        TimeLog[] GetTimeLogs(string lastName);
        void Add(TimeLog timeLog);
    }
}
