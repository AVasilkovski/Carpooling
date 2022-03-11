using Carpooling.Services.DTOs;
using Carpooling.Services.Exceptions;
using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers.Contracts;
using System.Threading.Tasks;

namespace Carpooling.Web.Helpers
{
    public class AuthHelper : IAuthHelper
    {
        private static readonly string ErrorMessage = "Incorrect username or password.";

        private readonly IUserService userService;

        public AuthHelper(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<UserPresentDTO> TryGetUserAsync(string credentialsHeader, string passwordHeader)
        {
            try
            {
                var user = await this.userService.GetUserByCredentialsAsync(credentialsHeader, passwordHeader);
                return user;
            }
            catch (EntityNotFoundException)
            {
                throw new EntityNotFoundException(ErrorMessage);
            }
        }
    }
}
