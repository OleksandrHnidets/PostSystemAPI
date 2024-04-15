using System;
using PostSystemAPI.DAL.Enums;

namespace PostSystemAPI.Domain.ViewModels;

public class LastPositionsViewModel
{
    public Guid Id { get; set; }
    public string TimeStamp { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public CurrentDriverStatus CurrentDriverStatus { get; set; }
    public DriverViewModel Driver { get; set; }
    public DeliveryModel Delivery { get; set; }
}

public class DriverViewModel
{
    public string DriverId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class DeliveryModel
{
    public Guid Id { get; set; }
    public string DeliveryName { get; set; }
    public string DeliveryDescription { get; set; }
    public string DeliveryDate { get; set; }
    public int Price { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
    public DeliveryType DeliveryType { get; set; }
    public bool IsFinished { get; set; }
    public string StartPostOfficeName { get; set; }
    public string StartPostOfficeAddress { get; set; }
    public string DestinationPostOfficeName { get; set; }
    public string DestinationPostOfficeAddress { get; set; }
}