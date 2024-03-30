using Shared.Models;

namespace Repository.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product?> GetProduct(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product?> UpdateProduct(Product product);
        Task<Product?> DeleteProduct(int id);

    }
}
