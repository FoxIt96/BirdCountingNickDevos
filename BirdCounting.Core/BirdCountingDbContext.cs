using Microsoft.EntityFrameworkCore;
using BirdCounting.Model;

namespace BirdCounting.Core
{
    public class BirdCountingDbContext : DbContext
    {
        public BirdCountingDbContext(DbContextOptions<BirdCountingDbContext> options) : base(options)
        {
        }

        public DbSet<Bird> Birds { get; set; }
        public DbSet<Session> Sessions { get; set; }

        // Configure the relationship between Session and Bird
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>()
                .HasMany(s => s.Birds)
                .WithOne()
                .HasForeignKey(b => b.SessionId);

            base.OnModelCreating(modelBuilder);
        }

        public void Seed()
        {
            // Assuming you have a SessionId property in the Bird entity to establish the relationship
            var session = new Session
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                Location = "Sample Location",
                IsActive = true
            };

            Sessions.Add(session);

            Birds.AddRange(new List<Bird>
            {
                new Bird { Name = "Boomkruiper", Description = "De boomkruiper is een kleine vogel, die met schokjes langs boomschors naar omhoog klautert, op zoek naar insecten en spinnen.", PhotoUrl = "/Images/Boomkruiper.jpg", SessionId = session.Id },
                new Bird { Name = "Gaai", Description = "De gaai is een luidruchtige, bont gekleurde vogel met een opvallend witte stuit en een lichtblauw vleugelveld. Hij legt een wintervoorraad aan van eikels.\r\n", PhotoUrl = "/Images/Gaai.jpg", SessionId = session.Id },
            });

            SaveChanges();
        }
    }
}
