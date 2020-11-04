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

namespace SmartcomStore.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public IdentityService(ApplicationDbContext dataContext, 
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

        public async Task<LoginResponseVM> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            
            if (user == null || user.IsDeleted )
                return new LoginResponseVM { Status = false, Error = "User with this username doesnt exist" };

            var resultLogin = await _userManager.CheckPasswordAsync(user, password);

            if (resultLogin)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                var userData = _mapper.Map<UserDto>(user);
                userData.Roles = userRole;
                return new LoginResponseVM { User = userData, Status = true };
            }
            else
            {
                return new LoginResponseVM { Status = false, Error = "Invalid password" };
            }
        }
        public async Task<RegistrationResponseVM> RegisterAsync(string username, string password, string name, string address)
        {
            var existingUser = await _userManager.FindByNameAsync(username);

            if (existingUser != null || existingUser.IsDeleted)
                return new RegistrationResponseVM { Status = false, Error = "This username also existed" };

            var newUser = new User
            {
                Address = address,
                UserName = username,
                Name = name
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);


            if (createdUser.Succeeded)
            {
                var userRole = await _userManager.AddToRoleAsync(newUser, Constants.Roles.Customer);
                return new RegistrationResponseVM{ Status = true };
            }
            else
            {
                return new RegistrationResponseVM { Status = false };
            }
        }
    }


}
