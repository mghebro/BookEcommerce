using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Entities.Books;
using EcommerceLearn.Domain.Enums.Books;
using MediatR;

namespace EcommerceLearn.Application.Features.Categories.Queries;

public sealed class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IQueryable<Category>>
{
    private readonly IDataContext _db;

    public GetAllCategoriesQueryHandler(IDataContext db)
    {
        _db = db;
    }

    public Task<IQueryable<Category>> Handle(
        GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var queryable = Enum
            .GetValues<Category>()
            .AsQueryable();

        return Task.FromResult(queryable);
    }
}