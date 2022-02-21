using System.Collections.Generic;
using Carpooling.Data;
using Carpooling.Data.Models;
using Carpooling.Services.Services.Contracts;

namespace Carpooling.Services.Services
{
    public class TravelTagService : ITravelTagService
    {
        private readonly CarpoolingContext dbContext;

        public TravelTagService(CarpoolingContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<TravelTag> GetAll()
        {
            return this.dbContext.TravelTags;
        }

        public ICollection<TravelTag> FindTags(IEnumerable<string> tags)
        {
            var travelTags = this.dbContext.TravelTags;
            var result = new List<TravelTag>();

            foreach (var item in travelTags)
            {
                foreach (var item1 in tags)
                {
                    if (item.Tag == item1)
                    {
                        result.Add(item);
                    }
                }
            }

            return result;
        }
    }
}
