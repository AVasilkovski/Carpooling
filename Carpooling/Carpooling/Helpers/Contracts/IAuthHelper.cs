using Carpooling.Services.DTOs;
using System.Threading.Tasks;

namespace Carpooling.Web.Helpers.Contracts
{
    public interface IAuthHelper
    {
        public Task<UserPresentDTO> TryGetUserAsync(string credentialsHeader, string passwordHeader);
    }
}
