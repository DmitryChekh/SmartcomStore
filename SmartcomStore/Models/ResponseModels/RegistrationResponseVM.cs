using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Models.ResponseModels
{
    public class RegistrationResponseVM : BaseResponseModel
    {
        public string Token { get; set; }
    }
}
