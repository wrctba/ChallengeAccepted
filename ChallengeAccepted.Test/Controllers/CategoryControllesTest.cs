using ChallengeAccepted.Controllers;
using ChallengeAccepted.DTO;
using ChallengeAccepted.Services;
using Moq;
using Xunit;

namespace ChallengeAccepted.Test.Controllers
{
    public class CategoryControllesTest
    {
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly CategoryController _categoryController;

        public CategoryControllesTest()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _categoryController = new CategoryController(_categoryServiceMock.Object);
        }

        [Fact]
        public async Task ListAsync_Success()
        {
            // Arrange
            var list = BaseList();
            _categoryServiceMock
               .Setup(x => x.ListAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string?>(), It.IsAny<string?>()))
               .Returns(Task.FromResult(list));

            // Act
            var response = await _categoryController.ListAsync();

            // Assert
            Assert.Equal(list, response);
        }

        [Fact]
        public async Task ListAsync_Pagination()
        {
            // Arrange
            var list = BaseList();
            _categoryServiceMock
               .Setup(x => x.ListAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string?>(), It.IsAny<string?>()))
               .Returns(Task.FromResult(list));

            // Act
            var response = await _categoryController.ListAsync("0", "101");

            // Assert
            Assert.Equal(list, response);
        }

        [Fact]
        public async Task GatAsync_Success()
        {
            // Arrange
            var category = BaseList()[0];
            _categoryServiceMock
               .Setup(x => x.GetAsync(It.IsAny<string>()))
               .Returns(Task.FromResult(category));

            // Act
            var response = await _categoryController.GetAsync("1");

            // Assert
            Assert.Equal(category, response);
        }

        [Fact]
        public async Task GetTreeAsync_Success()
        {
            // Arrange
            var tree = new CategoryTreeDto { Id = "1", Name = "Name 1", Children = new List<CategoryTreeDto> { new CategoryTreeDto { Id = "2", Name = "Name 2", ParentId = "1" } } };
            _categoryServiceMock
               .Setup(x => x.GetTreeAsync(It.IsAny<string>()))
               .Returns(Task.FromResult(tree));

            // Act
            var response = await _categoryController.GetTreeAsync("1");

            // Assert
            Assert.Equal(tree, response);
        }

        [Fact]
        public async Task PostAsync_Success()
        {
            // Arrange
            var postResponse = new CategoryPostResponseDto { Id = "2" };
            _categoryServiceMock
               .Setup(x => x.CreateAsync(It.IsAny<CategoryPostDto>()))
               .Returns(Task.FromResult(postResponse));

            // Act
            var response = await _categoryController.PostAsync(new CategoryPostDto());

            // Assert
            Assert.Equal(postResponse, response);
        }

        [Fact]
        public async Task PutAsync_Success()
        {
            // Arrange
            var categoryPutDto = new CategoryPutDto { Name = "Name" };

            // Act
            await _categoryController.PutAsync("1", categoryPutDto);

            // Assert
            _categoryServiceMock.Verify(x => x.UpdateAsync(It.Is<string>(id => id == "1"), It.Is<CategoryPutDto>(y => y  == categoryPutDto)));
        }

        [Fact]
        public async Task HideAsync_Success()
        {
            // Arrange

            // Act
            await _categoryController.HideAsync("1");

            // Assert
            _categoryServiceMock.Verify(x => x.UpdateVisibilityAsync(It.Is<string>(id => id == "1"), It.Is<bool>(y => y == false)));
        }

        [Fact]
        public async Task ShowAsync_Success()
        {
            // Arrange

            // Act
            await _categoryController.ShowAsync("1");

            // Assert
            _categoryServiceMock.Verify(x => x.UpdateVisibilityAsync(It.Is<string>(id => id == "1"), It.Is<bool>(y => y == true)));
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // Arrange

            // Act
            await _categoryController.DeleteAsync("1");

            // Assert
            _categoryServiceMock.Verify(x => x.DeleteAsync(It.Is<string>(id => id == "1")));
        }

        private static IList<CategoryDto> BaseList()
        {
            return new List<CategoryDto>{
                new CategoryDto{ Id = "1", Name = "Name 1", Slug = "Slug 1", Visible = true },
                new CategoryDto{ Id = "2", Name = "Name 2", Slug = "Slug 2", Visible = true, ParentId = "1" },
                new CategoryDto{ Id = "3", Name = "Name 3", Slug = null, Visible = true, ParentId = "1" },
                new CategoryDto{ Id = "4", Name = "Name 4", Slug = "Slug 4", Visible = true, ParentId = "2" },
                new CategoryDto{ Id = "5", Name = "Name 5", Slug = "Slug 5", Visible = true },
                new CategoryDto{ Id = "6", Name = "Name 6", Slug = null, Visible = false },
                new CategoryDto{ Id = "7", Name = "Name 7", Slug = "Slug 7", Visible = false },
            };
        }
    }
}
