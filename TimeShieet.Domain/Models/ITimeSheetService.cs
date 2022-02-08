
namespace TimeSheet.Domain.Models
{
    public interface ITimeSheetService
    {
        bool TrackTime(TimeLog timeLog);
    }
}