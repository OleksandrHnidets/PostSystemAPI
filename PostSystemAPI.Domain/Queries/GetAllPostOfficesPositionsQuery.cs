using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.Domain.ViewModels;

namespace PostSystemAPI.Domain.Queries;

public sealed record GetAllPostOfficesPositionsQuery(): IRequest<List<PostOfficeLocation>>;

public sealed class
    GetAllPostOfficesPositionsQueryHandler : IRequestHandler<GetAllPostOfficesPositionsQuery, List<PostOfficeLocation>>
{
    private readonly PostSystemContext _postSystemContext;

    public GetAllPostOfficesPositionsQueryHandler(PostSystemContext postSystemContext)
    {
        _postSystemContext = postSystemContext;
    }

    public async Task<List<PostOfficeLocation>> Handle(GetAllPostOfficesPositionsQuery request, CancellationToken cancellationToken)
    {
        return await _postSystemContext.PostOffices.Select(p => new PostOfficeLocation()
        {
            Id = p.Id,
            Address = p.Address,
            Latitude = p.Latitude,
            Longitude = p.Longitude,
            Name = p.Name,
        }).ToListAsync(cancellationToken);
    }
}