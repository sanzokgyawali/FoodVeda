using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVeda.Models
{
    public class PrimaryLocation
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string DeliveryCharge { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
