using PersonsDirectory.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly string _imageDirectory;

        public ImageService(string imageDirectory)
        {
            _imageDirectory = imageDirectory;
        }

        public async Task<string> SaveImageAsync(string fileName, Stream imageStream)
        {
            var filePath = Path.Combine(_imageDirectory, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageStream.CopyToAsync(fileStream);
            }
            return filePath;
        }

        public void DeleteImage(string fileName)
        {
            var filePath = Path.Combine(_imageDirectory, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<string> UpdateImageAsync(string fileName, Stream imageStream)
        {
            DeleteImage(fileName);
            return await SaveImageAsync(fileName, imageStream);
        }
    }
}
