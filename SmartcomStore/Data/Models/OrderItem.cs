using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Data.Models
{
    public class OrderItem :  BaseModel
    {
        public Guid OrderId { get; set; }
        public Guid ItemId { get; set; }

        [Required]
        public int ItemsCount { get; set; }
        [Required]
        [Column(TypeName = "decimal(14,2)")]
        public decimal ItemPrice { get; set; }

        public bool IsDeleted { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
