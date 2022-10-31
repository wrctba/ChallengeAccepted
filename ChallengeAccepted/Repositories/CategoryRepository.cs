using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ChallengeAccepted.DTO;
using ChallengeAccepted.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengeAccepted.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly ChallengeContext _context;

        public CategoryRepository(IMapper mapper, ChallengeContext challengeContext)
        {
            _mapper = mapper;
            _context = challengeContext;
        }

        public async Task<string> CreateAsync(CategoryPostDto categoryPostDto)
        {
            var category = _mapper.Map<Category>(categoryPostDto);
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }

        public async Task UpdateAsync(string id, CategoryPutDto categoryPutDto)
        {
            var category = await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                throw new ValidationException("Category not found");
            }
            _context.Entry(category).CurrentValues.SetValues(categoryPutDto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVisibilityAsync(string id, bool visible)
        {
            var category = await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                throw new ValidationException("Category not found");
            }
            category.Visible = visible;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.Category.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> CheckCircularRefAsync(string parentId, string? id = null)
        {
            var ids = new List<string>();
            if (id != null)
            {
                ids.Add(id);
            }

            var tempId = parentId;
            do
            {
                if (ids.Contains(tempId))
                {
                    return true;
                }
                ids.Add(tempId);
                tempId = await _context.Category.Where(x => x.Id == tempId).Select(x => x.ParentId).FirstOrDefaultAsync();
            } while (tempId != null);
            return false;
        }

        public async Task<CategoryDto> GetAsync(string id)
        {
            var category = await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                throw new ValidationException("Category not found");
            }
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<IList<CategoryDto>> GetChildrenAsync(string id)
        {
            var children = await _context.Category.Where(x => x.ParentId == id).ToListAsync();
            return _mapper.Map<IList<CategoryDto>>(children);
        }

        public async Task<IList<CategoryDto>> ListAsync(int page, int pageSize, string? name, string? slug)
        {
            var safePage = page > 0 ? page : 1;
            var query = _context.Category.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name)) {
                query = query.Where(x => x.Name.Contains(name));        
            }

            if (!string.IsNullOrWhiteSpace(slug))
            {
                query = query.Where(x => x.Slug != null && x.Slug.Contains(slug));
            }

            var list = await query.Skip((safePage - 1) * pageSize).Take(pageSize).ToListAsync();
            return _mapper.Map<IList<CategoryDto>>(list);
        }

        public async Task DeleteAsync(string id)
        {
            if (!await ExistsAsync(id))
            {
                throw new ValidationException("Category not found");
            }
            _context.Category.Remove(new Category { Id = id });
            await _context.SaveChangesAsync();
        }
    }
}
