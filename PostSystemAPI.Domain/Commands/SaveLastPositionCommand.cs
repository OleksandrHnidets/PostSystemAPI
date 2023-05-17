using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.DAL.Enums;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.Domain.ViewModels;

namespace PostSystemAPI.Domain.Commands;

public record SaveLastPositionCommand(DriverPositionMessage DriverPosition): IRequest<Position>;

public class SaveLastPositionCommandHandler : IRequestHandler<SaveLastPositionCommand, Position>
{
    private readonly PostSystemContext _postSystemContext;

    public SaveLastPositionCommandHandler(PostSystemContext postSystemContext)
    {
        _postSystemContext = postSystemContext;
    }

    public async Task<Position> Handle(SaveLastPositionCommand request, CancellationToken cancellationToken)
    {
        var existingPosition = _postSystemContext.Positions.FirstOrDefault(p =>
            p.DeliveryId.ToString() == request.DriverPosition.DeliveryId
            && p.UserId == request.DriverPosition.DriverId);

        if (existingPosition != null)
        {
            existingPosition.Latitude = request.DriverPosition.Latitude;
            existingPosition.Longitude = request.DriverPosition.Longitude;
            _postSystemContext.Update(existingPosition);
        }
        else
        {
            Position newPosition = new Position()
            {
                DeliveryId = Guid.Parse(request.DriverPosition.DeliveryId),
                UserId = request.DriverPosition.DriverId,
                Latitude = request.DriverPosition.Latitude,
                Longitude = request.DriverPosition.Longitude,
                CurrentDriverStatus = CurrentDriverStatus.Online,
                TimeStamp = DateTime.Now
            };

            _postSystemContext.Add(newPosition);
        }

        await _postSystemContext.SaveChangesAsync(cancellationToken);

        var position = _postSystemContext.Positions.FirstOrDefault(p => p.UserId == request.DriverPosition.DriverId);

        return position;
    }
}