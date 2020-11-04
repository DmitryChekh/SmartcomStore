using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartcomStore.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using SmartcomStore.Services.Interfaces;
using SmartcomStore.Models.RequetModels.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SmartcomStore.Models.RequestModels.Product;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Routing;
using SmartcomStore.Models;
using SmartcomStore.ResourceParameters;

namespace SmartcomStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProduct product)
        {
            var result = await _productService.CreateProductAsync(product.Name, product.Price, product.Category);

            if (!result.Status)
            {
                return BadRequest(result.Error);
            }

            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetById([Required] Guid id)
        {
            var result = await _productService.GetById(id);

            if (!result.Status)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Data);

        }
        [HttpGet("search/")]
        public async Task<IActionResult> GetProducts([FromQuery] ProductsSearchResourceParameters productsSearchResourceParameters)
        {
            var result = await _productService.GetProducts(productsSearchResourceParameters);

            if (!result.Status)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Data);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteProduct([FromQuery ,Required] Guid id)
        {
             var result = await _productService.DeleteProduct(id);

            if (!result.Status)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateProduct([FromBody, Required] ProductDto product)
        {

            var result = await _productService.UpdateProduct(product);

            if (!result.Status)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }

    }
}


