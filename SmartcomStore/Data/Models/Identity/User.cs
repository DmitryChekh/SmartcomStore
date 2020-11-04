using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Data.Models.Identity
{
    public class User : IdentityUser<Guid>
    {
        public User() {
            Id = Guid.NewGuid();
            // example: 2110-2020
            Code = $"{DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString()}-{DateTime.Now.Year.ToString()}";
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        [Required]
        public string Code { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Discount { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
 