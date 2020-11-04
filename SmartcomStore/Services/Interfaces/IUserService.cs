using SmartcomStore.Data.Models.Identity;
using SmartcomStore.Models;
using SmartcomStore.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponseModel<UserDto>> GetById(Guid id);

        Task<UserDto> GetUserByUsername(string username);
        Task<BaseResponseModel<IEnumerable<UserDto>>> GetUsers(int page, int perPage);
        Task<BaseResponseModel> DeleteUser(Guid id);

    }
}
