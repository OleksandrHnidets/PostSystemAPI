using System;
using PostSystemAPI.DAL.Enums;

namespace PostSystemAPI.DAL.Models;

public class Position
{
    public Guid Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public bool IsDriverOnline { get; set; }
    public CurrentDriverStatus CurrentDriverStatus { get; set; }
    public string UserId { get; set; }
    public Guid DeliveryId { get; set; }

    public virtual User User { get; set; }
    public virtual Delivery Delivery { get; set; }

}