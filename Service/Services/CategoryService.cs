using AutoMapper;
using Repository.Repositories;
using Shared.Dto;
using Shared.Models;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
     

        public async Task<List<Category>> GetCategories()
        {
            return await _categoryRepository.GetCategories();
        }

        public async Task<Category?> GetCategory(int id)
        {
            return await _categoryRepository.GetCategory(id);
        }

        public async Task<Category> CreateCategory(CategoryDto category)
        {
            Category newCategory = _mapper.Map<Category>(category);
            return await _categoryRepository.CreateCategory(newCategory);
        }

        public async Task<Category?> UpdateCategory(int id, CategoryDto category)
        {

            Category newCategory = _mapper.Map<Category>(category);
            return await _categoryRepository.UpdateCategory(newCategory);
        }

        public async Task<Category?> DeleteCategory(int id)
        {
            return await _categoryRepository.DeleteCategory(id);
        }


    }
}