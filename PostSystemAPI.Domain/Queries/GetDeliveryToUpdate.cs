using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.Domain.ViewModels;

namespace PostSystemAPI.Domain.Queries;

public record GetDeliveryToUpdate(string Id): IRequest<UpdateDeliveryViewModel>;

public sealed class GetDeliveryToUpdateQuery : IRequestHandler<GetDeliveryToUpdate, UpdateDeliveryViewModel>
{
    private readonly PostSystemContext _postSystemContext;

    public GetDeliveryToUpdateQuery(PostSystemContext postSystemContext)
    {
        _postSystemContext = postSystemContext;
    }

    public async Task<UpdateDeliveryViewModel> Handle(GetDeliveryToUpdate request, CancellationToken cancellationToken)
    {
        var entity = await _postSystemContext.Deliveries
            .Include(d => d.StartPostOffice)
            .Include(d => d.DestinationPostOffice)
            .Include(d => d.ReceivedUser)
            .FirstOrDefaultAsync(d => d.Id == Guid.Parse(request.Id), cancellationToken);

        if (entity == null)
        {
            return null;
        }

        var deliveryModel = new UpdateDeliveryViewModel()
        {
            Id = entity.Id.ToString(),
            DeliveryName = entity.DeliveryName,
            DeliveryDescription = entity.DeliveryDescription,
            DeliveryType = entity.DeliveryType,
            FromId = entity.StartPostOfficeId.ToString(),
            FromName = entity.StartPostOffice.Name,
            ToId = entity.DestinationPostOfficeId.ToString(),
            ToName = entity.DestinationPostOffice.Name,
            Price = entity.Price,
            ReceiverName = entity.ReceivedUser.FirstName + " " + entity.ReceivedUser.LastName
        };

        return deliveryModel;
    }
}