using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using admin.Dtos;
using admin.Models;
using admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace admin.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("role")]
        public async Task<IActionResult> CreateRole([FromBody] RoleRequest request)
        {
            var result = await _service.CreateRoleAsync(request);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _service.LoginAsync(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _service.RegisterAsync(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}