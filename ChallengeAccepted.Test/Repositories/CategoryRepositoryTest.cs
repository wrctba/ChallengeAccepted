using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ChallengeAccepted.DTO;
using ChallengeAccepted.Models;
using ChallengeAccepted.Repositories;
using Xunit;

namespace ChallengeAccepted.Test.Repositories
{
    public class CategoryRepositoryTest
    {
        private readonly IMapper _mapper;

        public CategoryRepositoryTest()
        {

            _mapper = MapperStart.StartAutoMapper();    
        }

        [Fact]
        public async Task CreateAsync_Succes()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());
            var categoryPostDto = new CategoryPostDto { Name = "Name 6" };

            // Act
            var result = await categoryRepository.CreateAsync(categoryPostDto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateAsync_Succes()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());
            var categoryPutDto = new CategoryPutDto { Name = "Name changed" };
            
            // Act
            await categoryRepository.UpdateAsync("1", categoryPutDto);

            // Assert
        }

        [Fact]
        public async Task UpdateAsync_Invalid_Id()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());
            var categoryPutDto = new CategoryPutDto { Name = "Name changed" };

            // Act
            var response = await Assert.ThrowsAsync<ValidationException>(() => categoryRepository.UpdateAsync("-1", categoryPutDto));

            // Assert
            Assert.Equal("Category not found", response.Message);
        }

        [Fact]
        public async Task UpdateVisibilityAsync_Succes()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            await categoryRepository.UpdateVisibilityAsync("1", false);

            // Assert
        }

        [Fact]
        public async Task UpdateVisibilityAsyncc_Invalid_Id()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());
            
            // Act
            var response = await Assert.ThrowsAsync<ValidationException>(() => categoryRepository.UpdateVisibilityAsync("-1", false));

            // Assert
            Assert.Equal("Category not found", response.Message);
        }

        [Fact]
        public async Task Exists_True()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            var result = await categoryRepository.ExistsAsync("1");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Exists_False()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            var result = await categoryRepository.ExistsAsync("-1");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CheckCircularRefAsync_True()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            var result = await categoryRepository.CheckCircularRefAsync("2", "1");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckCircularRefAsync_False()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            var result = await categoryRepository.CheckCircularRefAsync("5");

            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public async Task GetAsync_Succes()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            var response = await categoryRepository.GetAsync("1");

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetAsync_Invalid_Id()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            var response = await Assert.ThrowsAsync<ValidationException>(() => categoryRepository.GetAsync("-1"));

            // Assert
            Assert.Equal("Category not found", response.Message);
        }

        [Fact]
        public async Task GetChildrenAsync_Succes()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            var response = await categoryRepository.GetChildrenAsync("1");

            // Assert
            Assert.Equal(2, response.Count);
        }

        [Fact]
        public async Task ListAsync_Succes()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            var response = await categoryRepository.ListAsync(1, 20, null, null);

            // Assert
            Assert.Equal(5, response.Count);
        }


        [Fact]
        public async Task ListAsync_Invalid_Page()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            var response = await categoryRepository.ListAsync(-1, 20, null, null);

            // Assert
            Assert.Equal(5, response.Count);
        }

        [Fact]
        public async Task ListAsync_Page_size()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            var response = await categoryRepository.ListAsync(1, 2, null, null);

            // Assert
            Assert.Equal(2, response.Count);
        }

        [Fact]
        public async Task ListAsync_Page_skip()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            var response = await categoryRepository.ListAsync(2, 2, null, null);

            // Assert
            Assert.Equal(2, response.Count);
            Assert.Equal("3", response[0].Id);
        }

        [Fact]
        public async Task ListAsync_Name()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            var response = await categoryRepository.ListAsync(1, 20, "Name 1", null);

            // Assert
            Assert.Equal(3, response.Count);
        }

        [Fact]
        public async Task ListAsync_Slug()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            var response = await categoryRepository.ListAsync(1, 20, null, "Slug 4");

            // Assert
            Assert.Equal(1, response.Count);
        }

        [Fact]
        public async Task ListAsync_Name_Slug()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            var response = await categoryRepository.ListAsync(1, 20, "Name", "Slug");

            // Assert
            Assert.Equal(2, response.Count);
        }

        [Fact]
        public async Task DeleteAsync_Succes()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            await categoryRepository.DeleteAsync("1");

            // Assert
        }

        [Fact]
        public async Task DeleteAsync_Invalid_Id()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_mapper, GetContext());

            // Act
            var response = await Assert.ThrowsAsync<ValidationException>(() => categoryRepository.DeleteAsync("-1"));

            // Assert
            Assert.Equal("Category not found", response.Message);
        }


        private static ChallengeContext GetContext()
        {
            var categoryList = new List<Category> {
                new Category { Id = "1", Name = "Name 1", Slug = "Slug 1", Visible = true },
                new Category { Id = "2", Name = "Name 12", ParentId = "1", Visible = true },
                new Category { Id = "3", Name = "Name 13", ParentId = "1", Visible = true },
                new Category { Id = "4", Name = "Name 4", Slug = "Slug 4", ParentId = "3", Visible = true },
                new Category { Id = "5", Name = "Name 5", ParentId = "4", Visible = false },
            };
            return GetContext(categoryList);
        }

        private static ChallengeContext GetContext(List<Category> categoryList)
        {   
            var options = InMemoryDatabaseHelper.Create<ChallengeContext, Category>(categoryList);
            return new ChallengeContext(options);
        }
    }
}
