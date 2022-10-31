using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ChallengeAccepted.DTO;
using ChallengeAccepted.Repositories;

namespace ChallengeAccepted.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        } 

        public async Task<CategoryPostResponseDto> CreateAsync(CategoryPostDto categoryPostDto)
        {

            await ValidateAsync(_mapper.Map<CategoryPutDto>(categoryPostDto));

            var id = await _categoryRepository.CreateAsync(categoryPostDto);
            return new CategoryPostResponseDto { Id = id };
        }

        public async Task UpdateAsync(string id, CategoryPutDto categoryPutDto)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ValidationException("Invalid id");
            }
            await ValidateAsync(categoryPutDto, id);

            await _categoryRepository.UpdateAsync(id, categoryPutDto);
        }

        public async Task UpdateVisibilityAsync(string id, bool visible)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ValidationException("Invalid id");
            }
            await _categoryRepository.UpdateVisibilityAsync(id, visible);
        }

        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ValidationException("Invalid id");
            }
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<CategoryDto> GetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ValidationException("Invalid id");
            }

            return await _categoryRepository.GetAsync(id);
        }

        public async Task<IList<CategoryDto>> ListAsync(int page, int pageSize, string? name, string? slug)
        {
            return await _categoryRepository.ListAsync(page, pageSize, name, slug);
        }

        public async Task<CategoryTreeDto> GetTreeAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ValidationException("Invalid id");
            }

            var catagory = await _categoryRepository.GetAsync(id);
            var categoryTreeDto = _mapper.Map<CategoryTreeDto>(catagory);
            var children = await _categoryRepository.GetChildrenAsync(id);
            foreach(var child in children)
            {
                categoryTreeDto.Children.Add(await GetTreeAsync(child.Id));
            }
            return categoryTreeDto;
        }

        private async Task ValidateAsync(CategoryPutDto categoryPutDto, string? id = null)
        {
            if (string.IsNullOrWhiteSpace(categoryPutDto.Name))
            {
                throw new ValidationException("The attribute name is mandatory");
            }

            if (!string.IsNullOrWhiteSpace(categoryPutDto.ParentId))
            {
                if (!await _categoryRepository.ExistsAsync(categoryPutDto.ParentId))
                {
                    throw new ValidationException("ParentId not exists.");
                }

                if (await _categoryRepository.CheckCircularRefAsync(categoryPutDto.ParentId, id))
                {
                    throw new ValidationException("ParentId cause a circular reference.");
                }
            }
        }
    }
}
