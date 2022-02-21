using Microsoft.AspNetCore.Http;

namespace Carpooling.Web.Helpers.Contracts
{
    public interface IImageHelper
    {
        public string UploadImage(IFormFile registerViewModel);
    }
}
