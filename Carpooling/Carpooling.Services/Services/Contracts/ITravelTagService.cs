using Carpooling.Data.Models;
using System.Collections.Generic;

namespace Carpooling.Services.Services.Contracts
{
    public interface ITravelTagService
    {
        IEnumerable<TravelTag> GetAll();

        ICollection<TravelTag> FindTags(IEnumerable<string> tags);
    }
}
