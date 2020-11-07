using AutoMapper;
using SmartcomStore.Data.Models;
using SmartcomStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Profiles
{
    public class CreateOrderProfile : Profile
    {
        public CreateOrderProfile()
        {
            CreateMap<CreateOrderDto, Order>();
        }
    }
}
