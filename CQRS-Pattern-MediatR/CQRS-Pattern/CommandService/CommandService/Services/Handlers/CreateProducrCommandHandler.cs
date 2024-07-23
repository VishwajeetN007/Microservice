using CommandService.Database.Entities;
using CommandService.Services.Commands;
using MediatR;

namespace CommandService.Services.Handlers
{
    public class CreateProducrCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        AppDbContext _db;
        public CreateProducrCommandHandler(AppDbContext db)
        {
            _db = db;
        }
        public Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
           var product = new Product
           {
               Name = request.Name,
               Description = request.Description,
               UnitPrice = request.UnitPrice,
               ImageUrl = request.ImageUrl,
               CategoryId = request.CategoryId,
               CreatedDate = DateTime.Now
           };
            _db.Products.Add(product);
            _db.SaveChanges();
            return Task.FromResult(product.ProductId);
        }
    }
}
