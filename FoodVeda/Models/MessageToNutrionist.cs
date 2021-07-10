using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVeda.Models
{
    public class MessageToNutrionist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public virtual User User { get; set; }
        public string Status { get; set; }
    }
}
