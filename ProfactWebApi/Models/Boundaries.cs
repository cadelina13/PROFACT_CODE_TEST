using System.ComponentModel.DataAnnotations;

namespace ProfactWebApi.Models
{
    public class Boundaries
    {
        [Key]
        public int Id { get; set; }
        public string GlobalId { get; set; }
        public string Title { get; set; }
        public Coordinate Coordinate { get; set; } = new();
    }
}
