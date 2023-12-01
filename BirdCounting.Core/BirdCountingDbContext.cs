using Microsoft.EntityFrameworkCore;
using BirdCounting.Model;

namespace BirdCounting.Core
{
    public class BirdCountingDbContext : DbContext
    {
        public BirdCountingDbContext(DbContextOptions<BirdCountingDbContext> options) : base(options)
        {
        }
        public DbSet<Bird> Birds => Set<Bird>();
        public DbSet<Session> Sessions { get; set; }

        public void Seed()
        {
            Birds.AddRange(new List<Bird>
            {
                new Bird { Name = "Boomkruiper", Description = "De boomkruiper is een kleine vogel, die met schokjes langs boomschors naar omhoog klautert, op zoek naar insecten en spinnen.", PhotoUrl = "/Images/Boomkruiper.jpg" },
                new Bird { Name = "Gaai", Description = "De gaai is een luidruchtige, bont gekleurde vogel met een opvallend witte stuit en een lichtblauw vleugelveld. Hij legt een wintervoorraad aan van eikels.\r\n", PhotoUrl = "/Images/Gaai.jpg" },
            });

            SaveChanges();
        }
    }
}
