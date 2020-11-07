using SmartcomStore.Data.Models;
using SmartcomStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static SmartcomStore.Data.Models.Order;

namespace SmartcomStore.Models
{
    public class CreateOrderDto
    {
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public int OrderNumber { get; set; }
        [Required]
        public List<CreateOrderItemDto>  Items { get; set; }
    }

    public class CreateOrderItemDto
    {
        [Required]
        public Guid ItemId { get; set; }

        [Required]
        public int ItemsCount { get; set; } = 1;

    }
}
