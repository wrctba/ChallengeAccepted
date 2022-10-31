using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ChallengeAccepted.DTO;
using ChallengeAccepted.Repositories;
using ChallengeAccepted.Services;
using Moq;
using Xunit;

namespace ChallengeAccepted.Test.Services
{
    public class CategoryServiceTest
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly IMapper _mapper;
        private readonly CategoryService _categoryService;

        public CategoryServiceTest()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _mapper = MapperStart.StartAutoMapper();
            _categoryService = new CategoryService(_mapper, _categoryRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_Success()
        {
            // Arrange
            var categoryPostDto = new CategoryPostDto { Name = "name" };
            var id = "1";
            var categoryPostResponseDto = new CategoryPostResponseDto { Id = id };

            _categoryRepositoryMock
               .Setup(x => x.CreateAsync(It.IsAny<CategoryPostDto>()))
               .Returns(Task.FromResult(id));

            // Act
            var response = await _categoryService.CreateAsync(categoryPostDto);

            // Assert
            Assert.Equal(categoryPostResponseDto.Id, response.Id);
        }

        [Fact]
        public async Task CreateAsync_Invalid_Name()
        {
            // Arrange
            var categoryPostDto = new CategoryPostDto { Name = null };
            
            // Act
            var response = await Assert.ThrowsAsync<ValidationException>(() => _categoryService.CreateAsync(categoryPostDto));

            // Assert
            Assert.Equal("The attribute name is mandatory", response.Message);
        }

        [Fact]
        public async Task CreateAsync_Invalid_ParentId()
        {
            // Arrange
            var categoryPostDto = new CategoryPostDto { Name = "Name", ParentId = "invalid" };
            
            _categoryRepositoryMock
               .Setup(x => x.ExistsAsync(It.IsAny<string>()))
               .Returns(Task.FromResult(false));

            // Act
            var response = await Assert.ThrowsAsync<ValidationException>(() => _categoryService.CreateAsync(categoryPostDto));

            // Assert
            Assert.Equal("ParentId not exists.", response.Message);
        }

        [Fact]
        public async Task CreateAsync_Invalid_CircularRef()
        {
            // Arrange
            var categoryPostDto = new CategoryPostDto { Name = "Name", ParentId = "1" };

            _categoryRepositoryMock
               .Setup(x => x.ExistsAsync(It.IsAny<string>()))
               .Returns(Task.FromResult(true));

            _categoryRepositoryMock
               .Setup(x => x.CheckCircularRefAsync(It.IsAny<string>(), It.IsAny<string?>()))
               .Returns(Task.FromResult(true));

            // Act
            var response = await Assert.ThrowsAsync<ValidationException>(() => _categoryService.CreateAsync(categoryPostDto));

            // Assert
            Assert.Equal("ParentId cause a circular reference.", response.Message);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // Arrange
            var id = "1";
            var categoryPutDto = new CategoryPutDto { Name = "Name" };

            // Act
            await _categoryService.UpdateAsync(id, categoryPutDto);

            // Assert
            _categoryRepositoryMock
               .Verify(x => x.UpdateAsync(It.Is<string>(x => x == id), It.Is<CategoryPutDto>(y => y.Name == categoryPutDto.Name)));
        }

        [Fact]
        public async Task UpdateAsync_Invalid_Id()
        {
            // Arrange
            var id = "";
            var categoryPutDto = new CategoryPutDto { Name = "Name" };

            // Act
            var response = await Assert.ThrowsAsync<ValidationException>(() => _categoryService.UpdateAsync(id, categoryPutDto));

            // Assert
            Assert.Equal("Invalid id", response.Message);
        }

        [Fact]
        public async Task UpdateVisibilityAsync_Success()
        {
            // Arrange
            var id = "1";
            
            // Act
            await _categoryService.UpdateVisibilityAsync(id, true);

            // Assert
            _categoryRepositoryMock
               .Verify(x => x.UpdateVisibilityAsync(It.Is<string>(x => x == id), It.Is<bool>(y => y == true)));
        }

        [Fact]
        public async Task UpdateVisibilityAsync_Invalid_Id()
        {
            // Arrange
            var id = "";
            
            // Act
            var response = await Assert.ThrowsAsync<ValidationException>(() => _categoryService.UpdateVisibilityAsync(id, false));

            // Assert
            Assert.Equal("Invalid id", response.Message);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // Arrange
            var id = "1";

            // Act
            await _categoryService.DeleteAsync(id);

            // Assert
            _categoryRepositoryMock
               .Verify(x => x.DeleteAsync(It.Is<string>(x => x == id)));
        }

        [Fact]
        public async Task DeleteAsync_Invalid_Id()
        {
            // Arrange
            var id = "";

            // Act
            var response = await Assert.ThrowsAsync<ValidationException>(() => _categoryService.DeleteAsync(id));

            // Assert
            Assert.Equal("Invalid id", response.Message);
        }

        [Fact]
        public async Task GetAsync_Success()
        {
            // Arrange
            var id = "1";

            // Act
            await _categoryService.DeleteAsync(id);

            // Assert
            _categoryRepositoryMock
               .Verify(x => x.DeleteAsync(It.Is<string>(x => x == id)));
        }

        [Fact]
        public async Task GetAsync_Invalid_Id()
        {
            // Arrange
            var id = "";

            // Act
            var response = await Assert.ThrowsAsync<ValidationException>(() => _categoryService.GetAsync(id));

            // Assert
            Assert.Equal("Invalid id", response.Message);
        }

        [Fact]
        public async Task List_Success()
        {
            // Arrange
            var page = 1;
            var pageSize = 5;

            // Act
            await _categoryService.ListAsync(page, pageSize, null, null);

            // Assert
            _categoryRepositoryMock
               .Verify(x => x.ListAsync(It.Is<int>(x => x == page), It.Is<int>(x => x == pageSize), It.Is<string?>(x => x == null), It.Is<string?>(x => x == null)));
        }


        [Fact]
        public async Task GetTree_Success()
        {
            // Arrange
            var id = "1";
            var categoryDto = new CategoryDto { Id = "1", Name = "Name", };
            var categoryDto2 = new CategoryDto { Id = "2", Name = "Name 2", ParentId = "1" };
            _categoryRepositoryMock
               .SetupSequence(x => x.GetAsync(It.IsAny<string>()))
               .Returns(Task.FromResult(categoryDto))
               .Returns(Task.FromResult(categoryDto));

            IList<CategoryDto> children = new List<CategoryDto> { categoryDto2 };
            IList<CategoryDto> children2 = new List<CategoryDto>();
            _categoryRepositoryMock
               .SetupSequence(x => x.GetChildrenAsync(It.IsAny<string>()))
               .Returns(Task.FromResult(children))
               .Returns(Task.FromResult(children2));

            // Act
            var response = await _categoryService.GetTreeAsync(id);

            // Assert
            Assert.Equal(children.Count, response.Children.Count);
        }
    }
}
