using PersonsDirectory.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Tests.Infrastructure
{
    [TestFixture]
    public class ImageServiceTests
    {
        private ImageService _imageService;
        private string _imageDirectory;

        [SetUp]
        public void Setup()
        {
            _imageDirectory = Path.GetTempPath(); 
            _imageService = new ImageService(_imageDirectory);
        }

        [Test]
        public async Task SaveImageAsync_ShouldSaveImageToFileSystem()
        {
            var fileName = "testImage.jpg";
            var fileContent = new MemoryStream(Encoding.UTF8.GetBytes("fake image content"));

            var filePath = await _imageService.SaveImageAsync(fileName, fileContent);

            Assert.That(File.Exists(filePath), Is.True);

            File.Delete(filePath);
        }

        [Test]
        public void DeleteImage_ShouldDeleteImageIfExists()
        {
            var fileName = "testImage.jpg";
            var filePath = Path.Combine(_imageDirectory, fileName);
            File.WriteAllText(filePath, "fake content");

            _imageService.DeleteImage(fileName);

            Assert.That(File.Exists(filePath), Is.False);
        }

        [Test]
        public async Task UpdateImageAsync_ShouldDeleteAndSaveNewImage()
        {
            var fileName = "testImage.jpg";
            var initialFileContent = new MemoryStream(Encoding.UTF8.GetBytes("initial image content"));
            var updatedFileContent = new MemoryStream(Encoding.UTF8.GetBytes("updated image content"));

            await _imageService.SaveImageAsync(fileName, initialFileContent);

            var filePath = await _imageService.UpdateImageAsync(fileName, updatedFileContent);

            Assert.That(File.Exists(filePath), Is.True);

            var content = File.ReadAllText(filePath);
            Assert.That(content, Is.EqualTo("updated image content"));

            File.Delete(filePath);
        }

        [Test]
        public async Task SaveImageAsync_ShouldReturnCorrectFilePath()
        {
            var fileName = "testImage.jpg";
            var fileContent = new MemoryStream(Encoding.UTF8.GetBytes("fake image content"));

            var filePath = await _imageService.SaveImageAsync(fileName, fileContent);

            var expectedPath = Path.Combine(_imageDirectory, fileName);
            Assert.That(filePath, Is.EqualTo(expectedPath));

            File.Delete(filePath);
        }
    }
}
