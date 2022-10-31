using ChallengeAccepted.DTO;

namespace ChallengeAccepted.Repositories
{
    public interface ICategoryRepository
    {
        Task<bool> CheckCircularRefAsync(string parentId, string? id = null);
        Task<string> CreateAsync(CategoryPostDto categoryPostDto);
        Task DeleteAsync(string id);
        Task<bool> ExistsAsync(string id);
        Task<CategoryDto> GetAsync(string id);
        Task<IList<CategoryDto>> GetChildrenAsync(string id);
        Task UpdateAsync(string id, CategoryPutDto categoryPutDto);
        Task UpdateVisibilityAsync(string id, bool visible);
        Task<IList<CategoryDto>> ListAsync(int page, int pageSize, string? name, string? slug);
    }
}