using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.Domain.ViewModels;

namespace PostSystemAPI.Domain.Commands;

public record UpdateDeliveryCommand(UpdateDeliveryViewModel deliveryModel): IRequest<bool>;

public sealed class UpdateDeliveryCommandHandler : IRequestHandler<UpdateDeliveryCommand, bool>
{
    private readonly PostSystemContext _postSystemContext;
    private readonly UserManager<User> _userManager;

    public UpdateDeliveryCommandHandler(PostSystemContext postSystemContext, UserManager<User> userManager)
    {
        _postSystemContext = postSystemContext;
        _userManager = userManager;
    }

    public async Task<bool> Handle(UpdateDeliveryCommand request, CancellationToken cancellationToken)
    {
        var existingDelivery = await 
            _postSystemContext
                .Deliveries
                .FirstOrDefaultAsync(d => d.Id == Guid.Parse(request.deliveryModel.Id), cancellationToken);
        if (existingDelivery == null)
        {
            return false;
        }

        var receiver = await _userManager.FindByNameAsync(request.deliveryModel.ReceiverName.Replace(" ", "_"));

        if (receiver == null)
        {
            return false;
        }

        existingDelivery.DeliveryName = request.deliveryModel.DeliveryName;
        existingDelivery.DeliveryDescription = request.deliveryModel.DeliveryDescription;
        existingDelivery.Price = request.deliveryModel.Price;
        existingDelivery.ReceivedUser = receiver;

        _postSystemContext.Update(existingDelivery);
        await _postSystemContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}