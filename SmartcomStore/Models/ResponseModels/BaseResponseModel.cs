using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Models.ResponseModels
{
    public class BaseResponseModel
    {
        public bool Status { get; set; }
        public string Error { get; set; }
    }

    public class BaseResponseModel<T> : BaseResponseModel
    {
        public T Data { get; set; }
    }
}
