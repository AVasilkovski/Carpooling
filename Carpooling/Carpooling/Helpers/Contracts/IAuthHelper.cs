using Carpooling.Services.DTOs;

namespace Carpooling.Web.Helpers.Contracts
{
    public interface IAuthHelper
    {
        public UserPresentDTO TryGetUser(string credentialsHeader, string passwordHeader);
    }
}
