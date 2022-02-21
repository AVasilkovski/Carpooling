using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Data.Models
{
    public class TravelTag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Tag { get; set; }

        public ICollection<Travel> Travels { get; set; } = new List<Travel>();
    }
}
