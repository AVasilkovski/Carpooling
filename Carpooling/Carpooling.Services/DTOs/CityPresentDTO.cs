using Carpooling.Data.Models;

namespace Carpooling.Services.DTOs
{
    public class CityPresentDTO
    {
        public CityPresentDTO(City city)
        {
            City = city.Name;
        }

        public string City { get; set; }
    }
}
