using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.Domain.ViewModels;

namespace PostSystemAPI.Domain.Queries;

public record GetDriversQuery(): IRequest<List<DriverView>>;

public sealed class GetDriversQueryHandler : IRequestHandler<GetDriversQuery, List<DriverView>>
{
    private readonly PostSystemContext _postSystemContext;
    private readonly UserManager<User> _userManager;

    public GetDriversQueryHandler(PostSystemContext postSystemContext, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
    {
        _postSystemContext = postSystemContext;
        _userManager = userManager;
    }

    public async Task<List<DriverView>> Handle(GetDriversQuery request, CancellationToken cancellationToken)
    {
        var query = await _postSystemContext.Users.Include(u => u.AssignedDeliveries).ToListAsync();

        var drivers = new List<DriverView>();
        foreach (var user in query)
        {
            if (await _userManager.IsInRoleAsync(user, "Driver"))
            {
                var position = await _postSystemContext.Positions.OrderBy(p => p.TimeStamp)
                    .FirstOrDefaultAsync(p => p.UserId == user.Id);
                drivers.Add(new DriverView()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DriverStatus =  position == null ? DAL.Enums.CurrentDriverStatus.Offline : position.CurrentDriverStatus,
                    Deliveries = user.AssignedDeliveries.Select(d => new DriverDelivery()
                    {
                        DeliveryName = d.DeliveryName,
                        DeliveryType = d.DeliveryType
                    }).ToList(),
                    DeliveryCount = user.AssignedDeliveries.Count,
                });
            }
        }

        return drivers;

    }
}