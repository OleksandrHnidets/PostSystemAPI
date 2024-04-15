using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.Domain.ViewModels;

namespace PostSystemAPI.Domain.Queries;

public sealed record GetDeliveriesQuery(): IRequest<List<DeliveriesViewModel>>;

public sealed class GetDeliveriesQueryHandler : IRequestHandler<GetDeliveriesQuery, List<DeliveriesViewModel>>
{
    private readonly PostSystemContext _postSystemContext;

    public GetDeliveriesQueryHandler(PostSystemContext postSystemContext)
    {
        _postSystemContext = postSystemContext;
    }

    public async Task<List<DeliveriesViewModel>> Handle(GetDeliveriesQuery request, CancellationToken cancellationToken)
    {
        var query = await _postSystemContext.Deliveries
            .Include(d => d.SentUser)
            .Include(d => d.ReceivedUser)
            .Include(d => d.StartPostOffice)
            .Include(d => d.DestinationPostOffice)
            .Select(d => new DeliveriesViewModel()
            {
                Id = d.Id.ToString(),
                DeliveryName = d.DeliveryName,
                DeliveryDate = d.DeliveryDate,
                DeliveryStatus = d.DeliveryStatus,
                DeliveryType = d.DeliveryType,
                DeliveryDescription = d.DeliveryDescription,
                IsFinished = d.IsFinished,
                Price = d.Price,
                SentUser = d.SentUser.FirstName + " " + d.SentUser.LastName,
                ReceivedUser = d.ReceivedUser.FirstName + " " + d.ReceivedUser.LastName,

            }).ToListAsync(cancellationToken);
        return query;
    }
}