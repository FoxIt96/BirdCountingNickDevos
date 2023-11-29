using Microsoft.EntityFrameworkCore;
using BirdCounting.Model;
using System;

namespace BirdCounting.Core
{
    public class BirdCountingDbContext : DbContext
    {
        public BirdCountingDbContext(DbContextOptions<BirdCountingDbContext> options) : base(options)
        {
        }
        public DbSet<Bird> Birds => Set<Bird>();

        public void Seed()
        {
            Birds.AddRange(new List<Bird>
            {
                new Bird { Name = "John", Description = "Doe", PhotoUrl = "john.doe@example.com" },
                new Bird { Name = "Jane", Description = "Smith", PhotoUrl = "gfgffg" },
            });

            SaveChanges();
        }
    }
}
