using Microsoft.EntityFrameworkCore;
using Moq;
using PersonsDirectory.Infrastructure.Persistence;
using PersonsDirectory.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Tests.Infrastructure
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        private Mock<PersonsDirectoryDbContext> _dbContextMock;
        private UnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _dbContextMock = new Mock<PersonsDirectoryDbContext>(new DbContextOptions<PersonsDirectoryDbContext>());
            _unitOfWork = new UnitOfWork(_dbContextMock.Object);
        }

        [Test]
        public async Task CompleteAsync_ShouldCallSaveChangesAsync()
        {
            // Arrange
            _dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(1);

            // Act
            var result = await _unitOfWork.CompleteAsync();

            // Assert
            _dbContextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Dispose_ShouldCallDbContextDispose()
        {
            // Act
            _unitOfWork.Dispose();

            // Assert
            _dbContextMock.Verify(c => c.Dispose(), Times.Once);
        }
    }

}
