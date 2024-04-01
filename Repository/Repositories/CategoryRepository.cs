using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Shared.Models;

namespace Repository.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.Include(c => c.Products)
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Products = c.Products.Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price
                    }).ToList()
                }).ToListAsync();

        }

        public async Task<Category?> GetCategory(int id)
        {
            return await _context.Categories.Where(c => c.Id == id)
                .Include(c => c.Products)
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Products = c.Products.Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price
                    }).ToList()
                }).FirstOrDefaultAsync();                
        }

        public async Task<Category> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> UpdateCategory(Category category)
        {
            // find the category
            var categoryToUpdate = await _context.Categories.FindAsync(category.Id);


            if (categoryToUpdate == null)
            {
                return null;
            }
            // detach the category
            _context.Entry(categoryToUpdate).State = EntityState.Detached;
            // update the category
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;

        }

        public async Task<Category?> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return null;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}