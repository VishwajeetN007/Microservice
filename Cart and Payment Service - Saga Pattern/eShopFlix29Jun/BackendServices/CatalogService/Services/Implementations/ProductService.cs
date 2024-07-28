using CatalogService.Database;
using CatalogService.Database.Entities;
using CatalogService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Services.Implementations
{
    public class ProductService : IProductService
    {
        AppDbContext _db;
        IConfiguration _configuration;
        public ProductService(AppDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public void AddProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            _db.Products.Where(p => p.ProductId == id).ExecuteDelete();
        }

        public Product GetProduct(int id)
        {
            var product = _db.Products.Where(p => p.ProductId == id).FirstOrDefault();
            if (product != null)
            {
                product.ImageUrl = _configuration["ImageUrl"] + product.ImageUrl;
            }
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _db.Products.Select(p => new Product
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                UnitPrice = p.UnitPrice,
                CategoryId = p.CategoryId,
                ImageUrl = _configuration["ImageUrl"] + p.ImageUrl,
                CreatedDate = p.CreatedDate
            }).ToList();
        }

        public void UpdateProduct(Product product)
        {
            _db.Products.Update(product);
        }
    }
}
