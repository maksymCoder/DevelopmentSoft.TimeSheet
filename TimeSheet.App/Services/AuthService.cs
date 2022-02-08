using System;
using System.Collections.Generic;
using TimeSheet.Domain.Models;

namespace TimeSheet.App.Services
{
    public class AuthService : IAuthService
    {
        public List<string> Employees { get; private set; }

        public AuthService()
        {
            Employees = new List<string>()
            {
                "Иванов",
                "Сидоров",
                "Петров"
            };
        }
        public bool Login(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            var isEmployeeExist = Employees.Contains(lastName);
            if (isEmployeeExist)
            {
                UserSession.Sessions.Add(lastName);
            }
            return isEmployeeExist;
        }
    }
    public static class UserSession
    {
        public static HashSet<string> Sessions { get; set; }= new HashSet<string>();
    }
}
