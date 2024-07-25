using _3DTrackingProducts.Api.Core.Repositories;
using _3DTrackingProducts.Api.Data;
using _3DTrackingProducts.Api.Models;

namespace _3DTrackingProducts.Api.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> AddCategoryAsync(Category category)
        {
            await Add(category);
            return await Save();
        }


        public async Task<bool> DeleteCategoryAsync(int id)
        {
            Category? category = await GetByIdAsync(id);
            if(category == null)
            {
                return false;
            }
            await Remove(category);
            return await Save();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            List<Category> categories = (List<Category>) await GetAll();
            if(categories == null)
            {
               categories = new List<Category>();
            }
            return categories;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await SingleOrDefault(c => c.Id == id);
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await SingleOrDefault(c => c.Name == name);
        }

        public async Task<bool> UpdateCategoryAsync(int id,Category newCategory)
        {
            Category? category = await GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }
            category.Name = newCategory.Name;
            return await Save();
        }

    }
}
