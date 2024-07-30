using CatalogService.Database.Entities;

namespace CatalogService.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
    }
}
