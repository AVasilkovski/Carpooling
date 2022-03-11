using Carpooling.Data.Models.Enums;
using System.Collections.Generic;

namespace Carpooling.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public UserStatus UserStatus { get; set; }

        public IEnumerable<string> RolesName { get; set; }
    }
}
