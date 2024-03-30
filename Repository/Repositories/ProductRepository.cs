using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Exceptions;
using Shared.Models;

namespace Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteProduct(int id)
        {
            // check if the product exists
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }
            // remove the product
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> GetProduct(int id)
        {

            // get the product with the given id
            return await _context.Products.Include(p => p.ProductCategories).FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.Include(p => p.ProductCategories).ToListAsync();
        }

        public async Task<Product?> UpdateProduct(Product product)
        {
            // remove the old product categories
            _context.ProductCategories.RemoveRange(_context.ProductCategories.Where(pc => pc.ProductId == product.Id));
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
