using System.ComponentModel.DataAnnotations;

namespace ProfactWebApi.Models
{
    public class PostData
    {
        [Key]
        public int Id { get; set; }
        public Coordinate Northeast { get; set; }
        public Coordinate Southwest { get; set; }
        public int Zoom { get; set; }
        public string Search { get; set; }
    }
}
