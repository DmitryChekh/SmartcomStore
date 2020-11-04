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
using SmartcomStore.ResourceParameters;

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

        public async Task<BaseResponseModel<ProductDto>> CreateProductAsync(string name, decimal price, string category)
        {
            var product = new Product
            {
                Name = name,
                Price = price,
                Category = category
            };

            var productAdded = _dataContext.Products.Add(product);

            if(productAdded.State == EntityState.Added)
            {
                await _dataContext.SaveChangesAsync();
            }
            return new BaseResponseModel<ProductDto> { Status = true, Error = null, Data = _mapper.Map<ProductDto>(product) } ;

        }
        
        public async Task<BaseResponseModel> DeleteProduct(Guid id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return new BaseResponseModel { Status = false, Error = "The product was not deleted because it was not found." };

            product.IsDeleted = true;
            await _dataContext.SaveChangesAsync();

            return new BaseResponseModel { Status = true };

        }

        public async Task<BaseResponseModel<ProductDto>> GetById(Guid id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(q => q.Id == id && !q.IsDeleted);

            if(product == null)
            {
                return new BaseResponseModel<ProductDto> { Status = false, Error = "Product is not found" };
            }

            return new BaseResponseModel<ProductDto> { Status = true, Data = _mapper.Map<ProductDto>(product) };
        }



        //TODO: Решить нужен ли этот метод.
        public Task<IEnumerable<ProductDto>> GetProducts(int page, int perPage)
        {
            throw new NotImplementedException();
        }

        public  async Task<BaseResponseModel<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _dataContext.Products.AsNoTracking().Where(q => q.IsDeleted == false).ToListAsync();

            if (products.Count == 0)
                return new BaseResponseModel<IEnumerable<ProductDto>> { Status = false, Error = "Product list is empty" };

            return new BaseResponseModel<IEnumerable<ProductDto>> { Status = true, Data = _mapper.Map<IEnumerable<ProductDto>>(products) };
        }

        public async Task<BaseResponseModel<IEnumerable<ProductDto>>> GetProducts(ProductsSearchResourceParameters productsSearchResourceParameters)
        {
            if (string.IsNullOrWhiteSpace(productsSearchResourceParameters.Category)
                && string.IsNullOrWhiteSpace(productsSearchResourceParameters.Name))
            {
                return await GetProducts();
            }

            var collection = _dataContext.Products.AsNoTracking().Where(q => q.IsDeleted == false);

            if(!string.IsNullOrWhiteSpace(productsSearchResourceParameters.Category))
            {
                var category = productsSearchResourceParameters.Category.Trim();
                collection = collection.Where(a => a.Category.Contains(category));
            }

            if (!string.IsNullOrWhiteSpace(productsSearchResourceParameters.Name))
            {
                var name = productsSearchResourceParameters.Name.Trim();
                collection = collection.Where(a => a.Name.Contains(name));
            }

            var products = await collection.ToListAsync();

            return new BaseResponseModel<IEnumerable<ProductDto>> { Status = true, Data = _mapper.Map<IEnumerable<ProductDto>>(products) };
        }

        //public async Task<BaseResponseModel<IEnumerable<ProductDto>>> GetByName(string name)
        //{
        //    if (string.IsNullOrEmpty(name))
        //    {
        //        return await GetProducts();
        //    }

        //    var products = await _dataContext.Products.Where(q => q.Name.Contains(name) && !q.IsDeleted).ToListAsync();

        //    if (products == null)
        //    {
        //        return new BaseResponseModel<IEnumerable<ProductDto>> { Error = "Products contains this name are not found.", Status = false };
        //    }

        //    if (products.Count == 0)
        //    {
        //        return new BaseResponseModel<IEnumerable<ProductDto>> { Error = "Products contains this name are not found.", Status = false };
        //    }

        //    return new BaseResponseModel<IEnumerable<ProductDto>> { Status = true, Data = _mapper.Map<IEnumerable<ProductDto>>(products) };

        //}

        //public async Task<BaseResponseModel<IEnumerable<ProductDto>>> GetProductsByCategory(string category)
        //{
        //    if (string.IsNullOrEmpty(category))
        //    {
        //        return await GetProducts();
        //    }

        //    var products = await _dataContext.Products.Where(q => q.Category.Contains(category) && !q.IsDeleted).ToListAsync();

        //    if (products == null)
        //    {
        //        return new BaseResponseModel<IEnumerable<ProductDto>> { Error = "Products contains this name are not found.", Status = false };
        //    }

        //    if (products.Count == 0)
        //    {
        //        return new BaseResponseModel<IEnumerable<ProductDto>> { Error = "Products contains this name are not found.", Status = false };
        //    }

        //    return new BaseResponseModel<IEnumerable<ProductDto>> { Status = true, Data = _mapper.Map<IEnumerable<ProductDto>>(products) };
        //}


        public async Task<BaseResponseModel<ProductDto>> UpdateProduct(ProductDto product)
        {
            var updateProduct = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id.ToString() == product.Id);

            if (updateProduct == null)
                return new BaseResponseModel<ProductDto> { Status = false, Error = "Product is not found" };

            updateProduct.Name = product.Name;
            updateProduct.Price = product.Price;
            updateProduct.Category = product.Category;

            await _dataContext.SaveChangesAsync();

            return new BaseResponseModel<ProductDto> { Status = true, Data = _mapper.Map<ProductDto>(updateProduct) };
        }


    }
}