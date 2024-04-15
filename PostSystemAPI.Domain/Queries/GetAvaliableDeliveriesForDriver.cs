using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.Domain.ViewModels;

namespace PostSystemAPI.Domain.Queries;

public record GetAvaliableDeliveriesForDriver(string DriverId): IRequest<List<AvaliableDriverDeliveries>>;

public sealed class GetAvaliableDeliveriesForDriverHandler: IRequestHandler<GetAvaliableDeliveriesForDriver, List<AvaliableDriverDeliveries>>
{
    private readonly PostSystemContext _postSystemContext;

    public GetAvaliableDeliveriesForDriverHandler(PostSystemContext postSystemContext)
    {
        _postSystemContext = postSystemContext;
    }

    public async Task<List<AvaliableDriverDeliveries>> Handle(GetAvaliableDeliveriesForDriver request, CancellationToken cancellationToken)
    {
        return await _postSystemContext.Deliveries.Where(d => d.AssignedDriverId != request.DriverId)
            .Select(d => new AvaliableDriverDeliveries()
            {
                Id = d.Id.ToString(),
                Name = d.DeliveryName
            })
            .ToListAsync(cancellationToken);
    }
}