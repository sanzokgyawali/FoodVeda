using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVeda.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DisplayRatePerQuantity { get; set; }
        public string RateForFoodVeda { get; set; }
        public float Quantity { get; set; }
        public string BlobUrl { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int AverageRating { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
