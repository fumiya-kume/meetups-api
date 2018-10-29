﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using meetupsApi.JsonEntity;

namespace meetupsApi.Models
{
    public class meetupsApiContext : DbContext
    {
        public meetupsApiContext (DbContextOptions<meetupsApiContext> options)
            : base(options)
        {
        }

        public DbSet<meetupsApi.JsonEntity.Event> Event { get; set; }
    }
}
