using AutoMapper;
using SmartcomStore.Data.Models;
using SmartcomStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Profiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
