using CRMSystem.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
