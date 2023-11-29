using BirdCounting.Core;
using BirdCounting.Model;
using System;

namespace BirdCounting.Services
{
    public class BirdService
    {
        private readonly BirdCountingDbContext _dbContext;

        public BirdService(BirdCountingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Bird> Find()
        {
            return _dbContext.Birds
                .ToList();
        }

        public Bird? Get(int id)
        {
            return _dbContext.Birds
                .FirstOrDefault(p => p.Id == id);
        }

        public Bird? Create(Bird bird)
        {
            _dbContext.Birds.Add(bird);
            _dbContext.SaveChanges();

            return bird;
        }

        public Bird? Update(int id, Bird bird)
        {
            var dbBird = _dbContext.Birds.FirstOrDefault(p => p.Id == id);

            if (dbBird is null)
            {
                return null;
            }

            dbBird.Name = bird.Name;
            dbBird.Description = bird.Description;
            dbBird.PhotoUrl = bird.PhotoUrl;

            _dbContext.SaveChanges();

            return bird;
        }

        public void Delete(int id)
        {
            var dbBird = _dbContext.Birds.FirstOrDefault(p => p.Id == id);

            if (dbBird is null)
            {
                return ;
            }

            _dbContext.Birds.Remove(dbBird);

            _dbContext.SaveChanges();
        }
    }
}