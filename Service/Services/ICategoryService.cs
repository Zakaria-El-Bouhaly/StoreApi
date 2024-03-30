using Shared.Dto;
using Shared.Models;

namespace Service.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<Category?> GetCategory(int id);
        Task<Category> CreateCategory(CategoryDto category);
        Task<Category?> UpdateCategory(int id, CategoryDto category);
        Task<Category?> DeleteCategory(int id);
    }
}
