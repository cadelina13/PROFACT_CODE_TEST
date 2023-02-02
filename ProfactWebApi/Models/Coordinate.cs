using System.ComponentModel.DataAnnotations;

namespace ProfactWebApi.Models
{
    public class Coordinate
    {
        [Key]
        public int Id { get; set; }
        public double Lng { get; set; }
        public double Lat { get; set; }
    }
}
