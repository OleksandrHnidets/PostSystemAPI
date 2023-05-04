using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.Domain.ViewModels;

namespace PostSystemAPI.Domain.Queries;

public sealed record GetDriverDeliveries(string DriverId): IRequest<List<DriverDeliveriesViewModel>>;

public sealed class
    GetDriverDeliveriesQueryHandler : IRequestHandler<GetDriverDeliveries, List<DriverDeliveriesViewModel>>
{
    private readonly PostSystemContext _postSystemContext;

    public GetDriverDeliveriesQueryHandler(PostSystemContext postSystemContext)
    {
        _postSystemContext = postSystemContext;
    }

    public async Task<List<DriverDeliveriesViewModel>> Handle(GetDriverDeliveries request, CancellationToken cancellationToken)
    {
        var query = _postSystemContext.Deliveries
            .Include(d => d.StartPostOffice)
            .Include(d => d.DestinationPostOffice)
            .Where(d => d.AssignedDriverId == request.DriverId && !d.IsFinished);
        
        var deliveries = await query
            .Select(x => new DriverDeliveriesViewModel() 
            {
                Id = x.Id,
                Name = x.DeliveryName,
                Description = x.DeliveryDescription,
                From = new PostOfficeForDriver()
                {
                    Id = x.StartPostOffice.Id,
                    Address = x.StartPostOffice.Adress,
                    Name = x.StartPostOffice.Name
                },
                To = new PostOfficeForDriver()
                {
                    Id = x.DestinationPostOffice.Id,
                    Address = x.DestinationPostOffice.Adress,
                    Name = x.DestinationPostOffice.Name
                },
                DeliveryType = x.DeliveryType
            }).ToListAsync(cancellationToken);
        
        return deliveries;
    }
}