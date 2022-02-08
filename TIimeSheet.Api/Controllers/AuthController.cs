using Microsoft.AspNetCore.Mvc;
using System;
using TimeSheet.Api.ResourceModels;
using TimeSheet.Domain.Models;

namespace TimeSheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public ActionResult<bool> Login(LoginRequest request)
        {
            //var authService = new AuthService();
            return Ok(_authService.Login(request.LastName));
        }
    }
}
