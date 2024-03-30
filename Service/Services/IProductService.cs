using Shared.Dto;
using Shared.Models;

namespace Service.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product?> GetProduct(int id);
        Task<Product> CreateProduct(ProductDto product);
        Task<Product?> UpdateProduct(int id, ProductDto product);
        Task<Product?> DeleteProduct(int id);
    }
}