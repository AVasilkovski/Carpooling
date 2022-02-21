using System.Collections.Generic;

namespace Carpooling.Web.Models
{
    public class HomeViewModel
    {
        public int UsersCount { get; set; }

        public int TravelsCount { get; set; }

        public IEnumerable<UserHomeViewModel> Top10Passengers { get; set; }

        public IEnumerable<UserHomeViewModel> Top10Drivers { get; set; }
    }
}
