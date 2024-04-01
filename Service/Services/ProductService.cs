using AutoMapper;
using Service.Exceptions;
using Repository.Repositories;
using Shared.Dto;
using Shared.Models;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;


        public ProductService(IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }



        public async Task<List<Product>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<Product?> GetProduct(int id)
        {
            return await _productRepository.GetProduct(id);
        }

        public async Task<Product> CreateProduct(ProductDto product)
        {
            // Mapping the ProductDto to Product
            Product newProduct = _mapper.Map<Product>(product);
            newProduct.ProductCategories = new List<ProductCategory>();

            // Adding the ProductCategory to the Product
            foreach (var categoryId in product.CategoryIds)
            {
                // check if the category exists
                if (await _categoryRepository.GetCategory(categoryId) == null)
                {
                    throw new CategoryNotFoundException($"Category not found with id: {categoryId}");
                }

                newProduct.ProductCategories.Add(new ProductCategory { CategoryId = categoryId });
            }


            return await _productRepository.CreateProduct(newProduct);

        }

        public async Task<Product?> UpdateProduct(int id, ProductDto product)
        {
            Product newProduct = _mapper.Map<Product>(product);
            newProduct.ProductCategories = new List<ProductCategory>();

            // Adding the ProductCategory to the Product
            foreach (var categoryId in product.CategoryIds)
            {

                // check if the category exists
                
                if (await _categoryRepository.GetCategory(categoryId) == null)
                {
                    throw new CategoryNotFoundException($"Category not found with id: {categoryId}");
                }

                newProduct.ProductCategories.Add(new ProductCategory { CategoryId = categoryId });
            }

            return await _productRepository.UpdateProduct(newProduct);
        }

        public async Task<Product?> DeleteProduct(int id)
        {
            return await _productRepository.DeleteProduct(id);
        }


    }
}

