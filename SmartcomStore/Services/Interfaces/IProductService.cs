using SmartcomStore.Data.Models;
using SmartcomStore.Data.Models.Identity;
using SmartcomStore.Models;
using SmartcomStore.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateProduct(string code, string name, decimal price, string category);
        Task<ProductDto> GetById(Guid id);
        Task<IEnumerable<ProductDto>> GetProducts(int page, int perPage);
        Task<BaseResponseModel> DeleteProduct(Guid id);

    }
}
