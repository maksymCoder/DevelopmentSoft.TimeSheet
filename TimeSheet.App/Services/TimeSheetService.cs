using System;
using System.Collections.Generic;
using TimeSheet.Domain.Models;

namespace TimeSheet.App.Services
{
    public class TimeSheetService : ITimeSheetService
    {
        private readonly ITimeSheetRepository _timeSheetRepository;

        public TimeSheetService(ITimeSheetRepository timeSheetRepository)
        {
            _timeSheetRepository = timeSheetRepository;
        }
        public bool TrackTime(TimeLog timeLog)
        {
            bool isValid = (timeLog.WorkingHours > 0 && timeLog.WorkingHours <= 24)
                                  && (!string.IsNullOrWhiteSpace(timeLog.LastName));
            isValid = isValid && UserSession.Sessions.Contains(timeLog.LastName);

            if (isValid == false)
            {
                return false;
            }
            _timeSheetRepository.Add(timeLog);

            return true;
        }

    }
   
}
