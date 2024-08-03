using CatalogService.Services.Interfaces;
using CatalogService.Database;
using CatalogService.Database.Entities;

namespace CatalogService.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        AppDbContext _db;
        public CategoryService(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _db.Categories;
        }
    }
}
