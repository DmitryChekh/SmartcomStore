using Microsoft.AspNetCore.Identity;
using SmartcomStore.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Models
{
    public class UserDto 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }
        public decimal Discount { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
