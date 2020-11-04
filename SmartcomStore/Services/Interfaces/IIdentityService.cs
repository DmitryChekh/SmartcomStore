using SmartcomStore.Data.Models.Identity;
using SmartcomStore.Models;
using SmartcomStore.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<LoginResponseVM> LoginAsync(string username, string password);

        Task<RegistrationResponseVM> RegisterAsync(string username, string password, string name, string address);

    }
}
