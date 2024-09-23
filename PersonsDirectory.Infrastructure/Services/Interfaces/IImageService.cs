using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Infrastructure.Services.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(string fileName, Stream imageStream);
        void DeleteImage(string fileName);
        Task<string> UpdateImageAsync(string fileName, Stream imageStream);
    }
}
