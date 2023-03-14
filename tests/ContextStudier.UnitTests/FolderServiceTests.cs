using AutoMapper;
using ContextStudier.Core.Entitites;
using ContextStudier.Core.Exceptions;
using ContextStudier.Core.Interfaces.DataAccess;
using ContextStudier.Core.Services;
using Moq;

namespace ContextStudier.UnitTests
{
    public class FolderServiceTests
    {
        private readonly Mock<IRepository<Folder>> _repositoryMock;

        private readonly Mock<IMapper> _mapperMock;

        private readonly Mock<IRepositoryFactory> _repositoryFactoryMock;

        private readonly FolderService _sut;        

        public FolderServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Folder>>();
            _mapperMock = new Mock<IMapper>();
            _repositoryFactoryMock = new Mock<IRepositoryFactory>();
            _repositoryFactoryMock.Setup(f => f.GetRepository<Folder>()).Returns(_repositoryMock.Object);
            _sut = new FolderService(_repositoryFactoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task UpdateAsync_Updates_IfRequesterIsOwner()
        {
            //Arrange
            var folderId = 1;
            var userId = "user1";

            var folderNewStateMock = new Mock<Folder>(userId);
            folderNewStateMock.Setup(f => f.Id).Returns(folderId);

            var storedFolderMock = new Mock<Folder>(userId);
            storedFolderMock.Setup(f => f.Id).Returns(folderId);

            _repositoryMock.Setup(x => x.GetByIdAsync(folderNewStateMock.Object.Id, default))
                .ReturnsAsync(storedFolderMock.Object);
            _mapperMock.Setup(x => x.Map(folderNewStateMock.Object, storedFolderMock.Object))
                .Returns(folderNewStateMock.Object);

            //Act
            var result = await _sut.UpdateAsync(folderNewStateMock.Object);

            // Assert
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Folder>(), default), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ThrowsEntityWasNotStoredException_IfStoredFolderIsNull()
        {
            // Arrange
            var folderId = 1;
            var userId = "user1";

            var folderNewStateMock = new Mock<Folder>(userId);
            folderNewStateMock.Setup(f => f.Id).Returns(folderId);

            _repositoryMock.Setup(x => x.GetByIdAsync(folderId, default))
                .ReturnsAsync((Folder)null);

            // Act & Assert
            await Assert.ThrowsAsync<EntityWasNotStoredException>(() 
                => _sut.UpdateAsync(folderNewStateMock.Object));
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Folder>(), default), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ThrowsNotAllowedForRequesterException_IfUserIdsAreDifferent()
        {
            //Arrange
            var folderId = 1;
            var userId1 = "user1";
            var userId2 = "user2";

            var folderMock = new Mock<Folder>(userId1);
            folderMock.Setup(f => f.Id).Returns(folderId);

            var storedFolderMock = new Mock<Folder>(userId2);
            storedFolderMock.Setup(f => f.Id).Returns(folderId);

            _repositoryMock.Setup(x => x.GetByIdAsync(folderId, default))
                .ReturnsAsync(storedFolderMock.Object);

            //Act & Assert
            await Assert.ThrowsAsync<NotAllowedForRequesterException>(() => 
                _sut.UpdateAsync(folderMock.Object));
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Folder>(), default), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_DoNotTryToFindStoredFolder_IfFolderIsNew()
        {
            //Arrange
            var folderId = default(int);
            var folderMock = new Mock<Folder>("userId");
            folderMock.Setup(f => f.Id).Returns(folderId);

            //Act
            await _sut.UpdateAsync(folderMock.Object);

            //Assert
            _repositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>(), default), Times.Never);
        }
    }

}
