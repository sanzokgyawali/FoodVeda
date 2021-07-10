using FoodVeda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVeda.Dto
{
    public class HomeIndexDto
    {
        public HomeIndexDto()
        {
            TopProducts = new List<Product>();
            LatestProducts = new List<Product>();
        }
        public List<Product> TopProducts { get; set; }
        public List<Product> LatestProducts { get; set; }

    }
}
