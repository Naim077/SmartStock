using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class CategoryFacade : ICategoryFacade
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryFacade(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(long categoryId)
        {
            return await _categoryRepository.GetByIdAsync(categoryId);
        }

        public async Task AddCategoryAsync(Category category)
        {
            ArgumentNullException.ThrowIfNull(category);
            await _categoryRepository.AddAsync(category);
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            ArgumentNullException.ThrowIfNull(category);
            var existing = await _categoryRepository.GetByIdAsync(category.CategoryId);
            if (existing == null)
                return false;

            await _categoryRepository.UpdateAsync(category);
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(long categoryId)
        {
            var existing = await _categoryRepository.GetByIdAsync(categoryId);
            if (existing == null)
                return false;

            await _categoryRepository.DeleteAsync(categoryId);
            return true;
        }
    }
}
