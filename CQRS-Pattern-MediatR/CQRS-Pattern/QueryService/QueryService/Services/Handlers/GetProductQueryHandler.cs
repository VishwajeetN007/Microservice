using MediatR;
using QueryService.Database;
using QueryService.Database.Entities;
using QueryService.Services.Queries;

namespace QueryService.Services.Handlers
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
    {
        AppDbContext _db;
        public GetProductQueryHandler(AppDbContext db)
        {
            _db = db;
        }

        public Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            Product product = _db.Products.Find(request.ProductId);
            return Task.FromResult(product);
        }
    }
}
