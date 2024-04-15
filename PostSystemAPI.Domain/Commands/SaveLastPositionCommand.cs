using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.DAL.Enums;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.Domain.ViewModels;

namespace PostSystemAPI.Domain.Commands;

public record SaveLastPositionCommand(DriverPositionMessage DriverPosition): IRequest<UpdateMarkerViewModel>;

public class SaveLastPositionCommandHandler : IRequestHandler<SaveLastPositionCommand, UpdateMarkerViewModel>
{
    private readonly PostSystemContext _postSystemContext;
    private readonly IMapper _mapper;
    const double epsilon = 0.000001;

    public SaveLastPositionCommandHandler(PostSystemContext postSystemContext, IMapper mapper)
    {
        _postSystemContext = postSystemContext;
        _mapper = mapper;
    }

    public async Task<UpdateMarkerViewModel> Handle(SaveLastPositionCommand request, CancellationToken cancellationToken)
    {
        var existingPosition = _postSystemContext.Positions.FirstOrDefault(p =>
            p.DeliveryId.ToString() == request.DriverPosition.DeliveryId
            && p.UserId == request.DriverPosition.DriverId);

        if (existingPosition != null)
        {
            if (existingPosition.Latitude.Equals(request.DriverPosition.Latitude, epsilon)
                && existingPosition.Longitude.Equals(request.DriverPosition.Longitude, epsilon))
            {
                return _mapper.Map<UpdateMarkerViewModel>(existingPosition);
            }

            existingPosition.Latitude = request.DriverPosition.Latitude;
            existingPosition.Longitude = request.DriverPosition.Longitude;
            existingPosition.TimeStamp = DateTime.Now;
            _postSystemContext.Update(existingPosition);

            await _postSystemContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UpdateMarkerViewModel>(existingPosition);
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
            
            await _postSystemContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UpdateMarkerViewModel>(newPosition);
        }

    }
}


public static class MyExtensions
{
    public static bool Equals(this double value, double other, double epsilon)
    {
        return Math.Abs(value - other) < epsilon;
    }
}