using Microsoft.AspNetCore.Identity;
using SmartcomStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartcomStore.Data;
using SmartcomStore.Data.Models.Identity;
using SmartcomStore.Models;
using SmartcomStore.Core;
using AutoMapper;
using SmartcomStore.Models.ResponseModels;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SmartcomStore.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public UserService(ApplicationDbContext dataContext,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IMapper mapper
            )
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<BaseResponseModel> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user == null)
            {
                user.IsDeleted = true;
                await _dataContext.SaveChangesAsync();

                return new BaseResponseModel { Status = true };
            }

            return new BaseResponseModel { Status = false, Error = "User is not found" };
        }

        public async Task<BaseResponseModel<UserDto>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return new BaseResponseModel<UserDto> { Status = false, Error = "User is not found" };
            };

            if (user.IsDeleted)
            {
                return new BaseResponseModel<UserDto> { Status = false, Error = "User is deleted", Data = new UserDto() };
            }

            var userDto = _mapper.Map<UserDto>(user);

            userDto.Roles = await _userManager.GetRolesAsync(user);

            var result = new BaseResponseModel<UserDto>
            {
                Status = true,
                Error = null,
                Data = userDto
            };

            return result;
        }

        public Task<UserDto> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponseModel<IEnumerable<UserDto>>> GetUsers(int page, int perPage)
        {
            var qry = await _userManager.Users.Where(u => !u.IsDeleted).ToListAsync();

            var total = qry.Count();

            var users = qry
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList();

            if(users == null && users.Count == 0)
            {
                return new BaseResponseModel<IEnumerable<UserDto>>
                {
                    Data = new List<UserDto>(),
                    Status = false,
                    Error = "Something wrong"
                };
            }

            var result = new BaseResponseModel<IEnumerable<UserDto>>
            {
                Data = _mapper.Map<IEnumerable<UserDto>>(users),
                Status = true,
                Error = null
            };

            return result;
        }
    }
}
