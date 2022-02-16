using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeSheet.Domain.Models;

namespace TimeSheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeReportController : ControllerBase
    {
        private readonly IEmployeeReportService _employeeReportService;
        public EmployeeReportController(IEmployeeReportService employeeReportService)
        {
            _employeeReportService = employeeReportService; 
        }
        [HttpGet]
        public ActionResult<bool> GetEmployeeReport(string lastName)
        {
            return Ok(_employeeReportService.GetEmployeeReport(lastName));
        }
    }
}
