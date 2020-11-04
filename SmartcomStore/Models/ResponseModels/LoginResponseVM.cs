using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Models.ResponseModels
{
    public class LoginResponseVM : BaseResponseModel
    {
        public UserDto User { get; set; }
    }
}
