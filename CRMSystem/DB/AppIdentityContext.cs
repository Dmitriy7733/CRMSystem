﻿using CRMSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DB
{
    public sealed class AppIdentityContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<User> Users { get; set; }
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
        {
            
        }
    }
}
