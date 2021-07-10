using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVeda.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
           

        }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<PrimaryLocation> PrimaryLocations { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<MessageToNutrionist> MessageToNutrionists { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ProductComment> ProductComments { get; set; }
    }
}
