using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVeda.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LocationId { get; set; }
        public string FullAddress { get; set; }
        public int UserTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessName { get; set; }
        public string BusinessBlobUrl { get; set; }
        public string BlobUrl { get; set; }
        public string AboutUser { get; set; }
        public virtual PrimaryLocation Location { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<Product> Products { get; set; }



    }
}
