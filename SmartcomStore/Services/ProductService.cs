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
using SmartcomStore.Data.Models;

namespace SmartcomStore.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly IMapper _mapper;
        public ProductService(ApplicationDbContext dataContext,
            IMapper mapper
            )
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProduct(string code, string name, decimal price, string category)
        {
            var product = _dataContext.Products.Add(new Product
            {
                Code = code,
                Name = name,
                Price = price,
                Category = category
            });

            if(product.State == EntityState.Added)
            {
                await _dataContext.SaveChangesAsync();
            }
            return null;
        }

        public Task<BaseResponseModel> DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> GetProducts(int page, int perPage)
        {
            throw new NotImplementedException();
        }
    }
}
