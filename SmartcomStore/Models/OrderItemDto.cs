using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Models
{
    public class OrderItemDto
    {

        [Required]
        public int ItemsCount { get; set; }
        [Required]
        public decimal ItemPrice { get; set; }
        public ProductDto Product { get; set; }

    }
}
