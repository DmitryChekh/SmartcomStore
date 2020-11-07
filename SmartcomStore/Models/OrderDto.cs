using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Models
{
    public class OrderDto
    {
        public Guid CustomerId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public int OrderNumber { get; set; }
        public string Status { get; set; }

        public IEnumerable<OrderItemDto> Items { get; set; }
    }
}
