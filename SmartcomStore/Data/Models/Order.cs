using SmartcomStore.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Data.Models
{
    public class Order : BaseModel
    {
        public enum OrderStatus
        {
            New,
            Processing,
            Completed
        }

        public Guid CustomerId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public int OrderNumber { get; set; }
        public OrderStatus Status { get; set; }

        public bool IsDeleted { get; set; }
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
