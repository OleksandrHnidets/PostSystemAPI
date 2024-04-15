using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.Domain.ViewModels;

namespace PostSystemAPI.Domain.Queries;

public record GetPostOfficesForCreateCommand(): IRequest<List<PostOfficeForCreate>>;

public sealed class GetPostOfficesForCreateHandler : IRequestHandler<GetPostOfficesForCreateCommand, List<PostOfficeForCreate>>
{
    private readonly PostSystemContext _postSystemContext;

    public GetPostOfficesForCreateHandler(PostSystemContext postSystemContext)
    {
        _postSystemContext = postSystemContext;
    }

    public async Task<List<PostOfficeForCreate>> Handle(GetPostOfficesForCreateCommand request, CancellationToken cancellationToken)
    {
        return await _postSystemContext.PostOffices.Select(p => new PostOfficeForCreate()
        {
            Id = p.Id.ToString(),
            Name = p.Name
        }).ToListAsync(cancellationToken);
    }
}