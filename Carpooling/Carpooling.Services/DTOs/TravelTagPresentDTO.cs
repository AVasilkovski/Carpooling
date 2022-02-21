using Carpooling.Data.Models;

namespace Carpooling.Services.DTOs
{
    public class TravelTagPresentDTO
    {
        public TravelTagPresentDTO(TravelTag travelTag)
        {
            this.Id = travelTag.Id;
            this.Tag = travelTag.Tag;
        }

        public int Id { get; set; }

        public string Tag { get; set; }
    }
}