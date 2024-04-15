using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.DAL.Enums;
using PostSystemAPI.Domain.ViewModels;

namespace PostSystemAPI.Domain.Queries;

public record GetLastPositionsQuery() : IRequest<List<LastPositionsViewModel>>;

public sealed class GetLastPositionsQueryHandler : IRequestHandler<GetLastPositionsQuery, List<LastPositionsViewModel>>
{
    private readonly PostSystemContext _postSystemContext;

    public GetLastPositionsQueryHandler(PostSystemContext postSystemContext)
    {
        _postSystemContext = postSystemContext;
    }

    public async Task<List<LastPositionsViewModel>> Handle(GetLastPositionsQuery request, CancellationToken cancellationToken)
    {
        return await _postSystemContext.Positions
            .Include(p => p.User)
            .Include(p => p.Delivery)
            .ThenInclude(d => d.StartPostOffice)
            .Include(d => d.Delivery)
            .ThenInclude(p => p.DestinationPostOffice)
            .Where(p => p.CurrentDriverStatus == CurrentDriverStatus.Online)
            .Select(p => new LastPositionsViewModel()
            {
                Id = p.Id,
                TimeStamp = p.TimeStamp.ToString("G", CultureInfo.InvariantCulture),
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                CurrentDriverStatus = p.CurrentDriverStatus,
                Driver = new DriverViewModel()
                {
                    DriverId = p.User.Id,
                    FirstName = p.User.FirstName,
                    LastName = p.User.LastName,
                },
                Delivery = new DeliveryModel()
                {
                    Id = p.Delivery.Id,
                    DeliveryName = p.Delivery.DeliveryName,
                    DeliveryDescription = p.Delivery.DeliveryDescription,
                    DeliveryDate = p.Delivery.DeliveryDate.ToString("f", CultureInfo.InvariantCulture),
                    Price = p.Delivery.Price,
                    DeliveryStatus = p.Delivery.DeliveryStatus,
                    DeliveryType = p.Delivery.DeliveryType,
                    IsFinished = p.Delivery.IsFinished,
                    StartPostOfficeName = p.Delivery.StartPostOffice.Name,
                    StartPostOfficeAddress = p.Delivery.StartPostOffice.Address,
                    DestinationPostOfficeName = p.Delivery.DestinationPostOffice.Name,
                    DestinationPostOfficeAddress = p.Delivery.DestinationPostOffice.Address,
                }
            }).ToListAsync(cancellationToken);
    }
}