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
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetUsers(int page, int perPage)
        {
            if (!(page > 0 && perPage > 0))
                return BadRequest("Invalid page or perpage");

            var result = await _userService.GetUsers(page, perPage);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _userService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> DeleteUser(Guid id)
        //{
        //    var response = await _userService.DeleteUser(id);

        //    if (!response.Success)
        //    {
        //        Response.StatusCode = 404;
        //        await Response.WriteAsync(response.Errors.ToString());

        //        return Response;
        //    }

        //    return Ok();
        //}

    }
}


