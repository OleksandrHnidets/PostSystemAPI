using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.DAL.Models;

namespace PostSystemAPI.Domain.Commands;

public sealed record AssignDeliveryToDriverCommand(string DriverId, Guid DeliveryId) : IRequest<Result<Delivery>>;

public class AssignDeliveryToDriverHandler : IRequestHandler<AssignDeliveryToDriverCommand, Result<Delivery>>
{
    private readonly PostSystemContext _postSystemContext;

    public AssignDeliveryToDriverHandler(PostSystemContext postSystemContext)
    {
        _postSystemContext = postSystemContext;
    }

    public async Task<Result<Delivery>> Handle(AssignDeliveryToDriverCommand request, CancellationToken cancellationToken)
    {
        var delivery = _postSystemContext.Deliveries.FirstOrDefault(d => d.Id == request.DeliveryId);
        var driver = _postSystemContext.Users.FirstOrDefault(u => u.Id == request.DriverId);
        
        if(delivery != null && driver != null)
            driver.AssignedDeliveries.Add(delivery);

        await _postSystemContext.SaveChangesAsync(cancellationToken);

        return delivery;

    }
}