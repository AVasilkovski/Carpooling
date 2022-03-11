using System.Linq;
using System.Threading.Tasks;
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

        public async Task<string> CheckIfCityExistAsync(string cityName)
        {
            var result = this.dbContex.Cities.FirstOrDefault(city => city.Name == cityName);
            if (result == null)
            {
                await this.CreateCityAsync(cityName);
                return cityName;
            }
            else
            {
                return result.Name;
            }
        }

        public async Task<City> CreateCityAsync(string city)
        {
            var newCity = new City { Name = city };

            await this.dbContex.Cities.AddAsync(newCity);
            await this.dbContex.SaveChangesAsync();

            return newCity;
        }
    }
}
