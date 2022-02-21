using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Carpooling.Data.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
