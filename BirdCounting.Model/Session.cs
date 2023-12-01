
namespace BirdCounting.Model
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; } // Add this property
        public List<Bird> Birds { get; set; } = new List<Bird>();
    }
}
