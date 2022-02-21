using Carpooling.Services.DTOs;
using Carpooling.Services.Exceptions;
using Carpooling.Services.Services.Contracts;
using Carpooling.Web.Helpers.Contracts;

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

        public UserPresentDTO TryGetUser(string credentialsHeader, string passwordHeader)
        {
            try
            {
                var user = this.userService.GetUserByCredentials(credentialsHeader, passwordHeader);
                return user;
            }
            catch (EntityNotFoundException)
            {
                throw new EntityNotFoundException(ErrorMessage);
            }
        }
    }
}
