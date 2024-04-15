using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.DAL.Enums;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.Domain.ViewModels;

namespace PostSystemAPI.Domain.Commands;

public record CreateDeliveryCommand(CreateDeliveryViemModel CreateDelivery): IRequest<bool>;

public class CreateDeliveryCommandHandler : IRequestHandler<CreateDeliveryCommand, bool>
{
    private readonly PostSystemContext _postSystemContext;
    private readonly UserManager<User> _userManager;

    public CreateDeliveryCommandHandler(PostSystemContext postSystemContext, UserManager<User> userManager)
    {
        _postSystemContext = postSystemContext;
        _userManager = userManager;
    }

    public async Task<bool> Handle(CreateDeliveryCommand request, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByNameAsync(request.CreateDelivery.ReceiverName.Replace(" ", "_")) == null)
        {
            return false;
        }
        
        if (await _userManager.FindByNameAsync(request.CreateDelivery.SenderName) == null)
        {
            return false;
        }

        var sender = await _userManager.FindByNameAsync(request.CreateDelivery.SenderName);
        var receiver = await _userManager.FindByNameAsync(request.CreateDelivery.ReceiverName.Replace(" ", "_"));

        var newDelivery = new Delivery()
        {
            Id = new Guid(),
            DeliveryName = request.CreateDelivery.DeliveryName,
            DeliveryDescription = request.CreateDelivery.DeliveryDescription,
            Price = request.CreateDelivery.Price,
            DeliveryType = request.CreateDelivery.DeliveryType,
            DeliveryDate = DateTime.Now,
            DeliveryStatus = DeliveryStatus.WaitingToAccept,
            SentUserId = sender!.Id,
            ReceivedUserId = receiver!.Id,
            StartPostOfficeId = Guid.Parse((ReadOnlySpan<char>)request.CreateDelivery.FromId),
            DestinationPostOfficeId = Guid.Parse(request.CreateDelivery.ToId)
        };

        await _postSystemContext.AddAsync(newDelivery, cancellationToken);

        await _postSystemContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}