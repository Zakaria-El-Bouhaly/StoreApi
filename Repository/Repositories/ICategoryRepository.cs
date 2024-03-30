using Shared.Models;

namespace Repository.Repositories
{
    public interface ICategoryRepository
    {
       
        Task<List<Category>> GetCategories();
        Task<Category?> GetCategory(int id);
        Task<Category> CreateCategory(Category category);
        Task<Category?> UpdateCategory(Category category);
        Task<Category?> DeleteCategory(int id);
        

    }
}
