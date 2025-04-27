using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManger serviceManger):ControllerBase
    {
        //login
        [HttpPost("login")] // Post:  /api/auth/login
        public async Task<IActionResult> Login (LoginDto loginDto){ 
        var result= await serviceManger.AuthService.LoginAsync(loginDto);
            return Ok(result);
        
        }
        //register
        [HttpPost("register")] // Post:  /api/auth/register
        public async Task<IActionResult> Register(RegisterDto RegisterDto)
        {
            var result = await serviceManger.AuthService.RegisterAsync(RegisterDto);
            return Ok(result);

        }
    }
}
