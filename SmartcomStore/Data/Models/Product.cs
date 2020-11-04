using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Data.Models
{
    public class Product : BaseModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(14,2)")]
        public decimal Price { get; set; }

        [MaxLength(30)]
        [Required]
        public string Category { get; set; }
    }
}
