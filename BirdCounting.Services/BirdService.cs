using BirdCounting.Core;
using BirdCounting.Model;
using System;
using static System.Collections.Specialized.BitVector32;

namespace BirdCounting.Services
{
    public class BirdService
    {
        private readonly BirdCountingDbContext _dbContext;
        private int _currentSessionId; // Declare _currentSessionId within the class


        public BirdService(BirdCountingDbContext dbContext)
        {
            _dbContext = dbContext;
            _currentSessionId = GetLatestSessionId();

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
                return;
            }

            _dbContext.Birds.Remove(dbBird);

            _dbContext.SaveChanges();
        }

        public List<Session> GetSessions()
        {

            return _dbContext.Sessions.ToList();
        }

        public Session? CreateSession(Session session)
        {
            session.IsActive = true;
            _dbContext.Sessions.Add(session);
            _dbContext.SaveChanges();

            return session;
        }

        public Session GetSessionDetails(int id)
        {
            return _dbContext.Sessions.FirstOrDefault(s => s.Id == id);
        }

        public void StopSession(int sessionId)
        {
            var session = _dbContext.Sessions.FirstOrDefault(s => s.Id == sessionId);

            if (session != null)
            {
                session.IsActive = false;
                session.EndTime = DateTime.Now; // Set the end time when stopping the session
                _dbContext.SaveChanges();
            }
            // You might want to handle the case where the session is not found (optional).
        }

        public List<Bird> GetBirdsSortedByFrequency()
        {
            // Assuming you have a property named Count in your Bird model
            return _dbContext.Birds.OrderByDescending(b => b.Count).ToList();
        }

        public void CountBird(int birdId, int countChange)
        {
            var bird = _dbContext.Birds.FirstOrDefault(b => b.Id == birdId);

            if (bird != null)
            {
                bird.Count += countChange;
                bird.Count = Math.Max(bird.Count, 0);
                _dbContext.SaveChanges();
            }
            // You might want to handle the case where the bird is not found (optional).
        }

        public int GetCurrentSessionId()
        {
            return _currentSessionId;
        }

        private int GetLatestSessionId()
        {
            // Retrieve the ID of the latest active session from the database
            var latestSession = _dbContext.Sessions
                .OrderByDescending(s => s.StartTime)
                .FirstOrDefault(s => s.IsActive);

            return latestSession?.Id ?? 0;
        }

        public Session GetCurrentActiveSession()
        {
            return _dbContext.Sessions.FirstOrDefault(s => s.IsActive);
        }

    }
}