using Carpooling.Web.Helpers.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Carpooling.Web.Helpers
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public ImageHelper(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public string UploadImage(IFormFile profileImage)
        {
            string uniqueFileName = null;

            if (profileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid() + "_" + profileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    profileImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
