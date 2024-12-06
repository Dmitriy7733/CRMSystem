using CRMSystem.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Event> Events { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Events)
                .WithOne(e => e.Client)
                .HasForeignKey(e => e.ClientId);
        }
    }
}
