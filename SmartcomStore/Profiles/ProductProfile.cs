using AutoMapper;
using SmartcomStore.Data.Models;
using SmartcomStore.Data.Models.Identity;
using SmartcomStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Profiles
{
    public class ProductProfie : Profile
    {
        public ProductProfie()
        {
            CreateMap<Product, ProductDto>();
        }

    }
}
