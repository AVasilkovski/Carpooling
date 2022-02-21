using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Data.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Travel> TravelStartPoints { get; set; } = new List<Travel>();

        public ICollection<Travel> TravelEndPoints { get; set; } = new List<Travel>();
    }
}
