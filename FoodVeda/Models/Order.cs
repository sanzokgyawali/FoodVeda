using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVeda.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public float Quantity { get; set; }
        public int TotalPrice { get; set; }
        public int LocationId { get; set; }
        public string FullAddress { get; set; }
        public int StatusId { get; set; }
        public string OrderIdForTrackingStatus { get; set; }
        public virtual Status Status { get; set; }
        public virtual Product Product { get; set; }
        public virtual PrimaryLocation Location { get; set; }

    }
}
