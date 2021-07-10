﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVeda.Models
{
    public class UserType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
