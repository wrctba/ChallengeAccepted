using ChallengeAccepted.DTO;

namespace ChallengeAccepted.Services
{
    public interface ICategoryService
    {
        Task<CategoryPostResponseDto> CreateAsync(CategoryPostDto categoryPostDto);
        Task DeleteAsync(string id);
        Task<CategoryDto> GetAsync(string id);
        Task<CategoryTreeDto> GetTreeAsync(string id);
        Task UpdateAsync(string id, CategoryPutDto categoryPutDto);
        Task UpdateVisibilityAsync(string id, bool visible);
        Task<IList<CategoryDto>> ListAsync(int page, int pageSize, string? name, string? slug);
    }
}