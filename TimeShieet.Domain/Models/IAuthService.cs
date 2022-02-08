using System.Collections.Generic;

namespace TimeSheet.Domain.Models
{
    public interface IAuthService
    {
        List<string> Employees { get; }

        bool Login(string lastName);
    }
}