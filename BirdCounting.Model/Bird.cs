
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace BirdCounting.Model
{
    [Table(nameof(Bird))]
    public class Bird
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
    }
}
