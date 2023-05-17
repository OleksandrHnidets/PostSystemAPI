using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using PostSystemAPI.Domain.Commands;
using PostSystemAPI.Domain.ViewModels;

namespace PostSystemAPI.SignalR;

public class LiveMapHub : Hub
{
    private readonly IMediator _mediator;

    public LiveMapHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task SendUserPositions(DriverPositionMessage message)
    {
        var position = await _mediator.Send(new SaveLastPositionCommand(message));
        
        await Clients.All.SendAsync("UpdateDriverCoordinates", position);
    }
}