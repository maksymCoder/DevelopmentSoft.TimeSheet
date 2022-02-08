using System;
using System.Collections.Generic;
using TimeSheet.Domain.Models;

namespace TimeSheet.App.Services
{
    public class TimeSheetService : ITimeSheetService
    {
        public bool TrackTime(TimeLog timeLog)
        {
            bool isValid = (timeLog.WorkingHours > 0 && timeLog.WorkingHours <= 24)
                                  && (!string.IsNullOrWhiteSpace(timeLog.LastName));
            isValid = isValid && UserSession.Sessions.Contains(timeLog.LastName);

            if (isValid == false)
            {
                return false;
            }
            TimeSheets.TimeLogs.Add(timeLog);

            return true;
        }

    }
    public static class TimeSheets
    {
        public static List<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
    }
}
