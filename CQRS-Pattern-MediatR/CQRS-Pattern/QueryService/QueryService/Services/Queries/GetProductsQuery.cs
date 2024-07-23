using MediatR;
using QueryService.Database.Entities;

namespace QueryService.Services.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {

    }
}
