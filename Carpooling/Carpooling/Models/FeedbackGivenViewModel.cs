using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Web.Models
{
    public class FeedbackGivenViewModel 
    {
        public int UserId { get; set; }

        public string UserFromUsername { get; set; }

        public string Comment { get; set; }

        public double Rating { get; set; }
    }
}
