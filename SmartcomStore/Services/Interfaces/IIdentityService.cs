using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<int> GetUserByUsername();

        Task<int> GetUserById();

        Task<int> CreateUser();
    }
}
