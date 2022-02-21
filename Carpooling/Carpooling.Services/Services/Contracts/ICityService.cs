using Carpooling.Data.Models;

namespace Carpooling.Services.Services.Contracts
{
    public interface ICityService
    {
        City CreateCity(string city);

        City CheckIfCityExist(string city);
    }
}
