using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVeda.Models
{
    public class ProductComment
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Rating { get; set; }
    }
}
