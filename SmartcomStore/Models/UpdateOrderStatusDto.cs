using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Models
{
    public class UpdateOrderStatusDto
    {
        public Guid Id { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string Status { get; set; }
    }
}
