using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartcomStore.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using SmartcomStore.Services.Interfaces;
using SmartcomStore.Models.RequetModels.Identity;

namespace SmartcomStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegRequest request)
        {
            var result = await _identityService.RegisterAsync(
                request.Username,
                request.Password,
                request.Name,
                request.Address
                );

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _identityService.LoginAsync(request.Username, request.Password);

            if (result == null)
                return BadRequest("Any problems");

            if (!result.Status)
                return BadRequest(result.Error);

            return Ok(result);
        }


        //[HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Test()
        {
            return Ok("Security");
        }





        //[HttpPost]
        //public async Task<IActionResult> Login()
        //{
        //    var secretBytes = Encoding.UTF8.GetBytes(Constants.JwtOptions.Secret);
        //    var key = new SymmetricSecurityKey(secretBytes);

        //    var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Expires = DateTime.UtcNow.AddHours(2),

        //        SigningCredentials = signingCredentials
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);

        //    var result = tokenHandler.WriteToken(token);

        //    return Ok(result);
        //}

    }
}