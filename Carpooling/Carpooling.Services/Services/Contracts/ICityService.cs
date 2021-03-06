using Carpooling.Data.Models;
using System.Threading.Tasks;

namespace Carpooling.Services.Services.Contracts
{
    public interface ICityService
    {
        Task<City> CreateCityAsync(string city);

        Task<string> CheckIfCityExistAsync(string city);
    }
}
