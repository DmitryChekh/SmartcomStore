using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Models
{
    public class ConfirmOrderDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime ShipmentDate { get; set; }
    }
}
