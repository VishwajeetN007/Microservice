using MediatR;
using QueryService.Database;
using QueryService.Database.Entities;
using QueryService.Services.Queries;

namespace QueryService.Services.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        AppDbContext _db;
        public GetProductsQueryHandler(AppDbContext db)
        {
            _db = db;
        }

        public Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _db.Products.ToList();
            return Task.FromResult<IEnumerable<Product>>(products);
        }
    }
}
