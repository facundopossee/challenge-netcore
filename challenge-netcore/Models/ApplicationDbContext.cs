﻿using Microsoft.EntityFrameworkCore;
using challenge_netcore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge_netcore.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
    }
}
