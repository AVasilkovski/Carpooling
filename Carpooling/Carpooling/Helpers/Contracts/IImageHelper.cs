using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Carpooling.Web.Helpers.Contracts
{
    public interface IImageHelper
    {
        public Task<string> UploadImageAsync(IFormFile registerViewModel);
    }
}
