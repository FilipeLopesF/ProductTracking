using _3DTrackingProducts.Api.Models;

namespace _3DTrackingProducts.Api.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> GetAllCategoriesAsync();

        Task<Category?> GetByIdAsync(int id);

        Task<Category?> GetByNameAsync(string name);

        Task<bool> AddCategoryAsync(Category category);

        Task<bool> UpdateCategoryAsync(int id,Category category);

        Task<bool> DeleteCategoryAsync(int id);
    }
}
