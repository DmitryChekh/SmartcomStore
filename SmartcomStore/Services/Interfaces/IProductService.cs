using SmartcomStore.Data.Models;
using SmartcomStore.Data.Models.Identity;
using SmartcomStore.Models;
using SmartcomStore.Models.ResponseModels;
using SmartcomStore.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Services.Interfaces
{
    public interface IProductService
    {
        Task<BaseResponseModel<ProductDto>> CreateProductAsync(string name, decimal price, string category);
        Task<BaseResponseModel<ProductDto>> GetById(Guid id);
        Task<IEnumerable<ProductDto>> GetProducts(int page, int perPage);

        Task<BaseResponseModel<IEnumerable<ProductDto>>> GetProducts();

        Task<BaseResponseModel<IEnumerable<ProductDto>>> GetProducts(ProductsSearchResourceParameters productsSearchResourceParameters);
        Task<BaseResponseModel> DeleteProduct(Guid id);
        Task<BaseResponseModel<ProductDto>> UpdateProduct(ProductDto product);
    }
}
