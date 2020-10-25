using Microsoft.AspNetCore.Identity;
using SmartcomStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartcomStore.Data;
using SmartcomStore.Data.Models.Identity;

namespace SmartcomStore.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public IdentityService(ApplicationDbContext dataContext, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<int> CreateUser()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetUserById()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetUserByUsername()
        {
            throw new NotImplementedException();
        }
    }
}
