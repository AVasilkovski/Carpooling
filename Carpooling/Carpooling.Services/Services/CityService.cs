using System.Linq;
using Carpooling.Data;
using Carpooling.Data.Models;
using Carpooling.Services.Services.Contracts;

namespace Carpooling.Services.Services
{
    public class CityService : ICityService
    {
        private readonly CarpoolingContext dbContex;

        public CityService(CarpoolingContext dbContex)
        {
            this.dbContex = dbContex;
        }

        public City CheckIfCityExist(string cityName)
        {
            var result = this.dbContex.Cities.FirstOrDefault(city => city.Name == cityName);
            if (result == null)
            {
                return this.CreateCity(cityName);
            }
            else
            {
                return result;
            }
        }

        public City CreateCity(string city)
        {
            var newCity = new City { Name = city };

            this.dbContex.Cities.Add(newCity);
            this.dbContex.SaveChanges();

            return newCity;
        }
    }
}
