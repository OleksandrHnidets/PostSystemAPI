using System;
using System.Collections.Generic;
using PostSystemAPI.DAL.Enums;

namespace PostSystemAPI.Domain.ViewModels;

public class DriverDeliveriesViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public PostOfficeForDriver From { get; set; }
    public PostOfficeForDriver To { get; set; }
    public DeliveryType DeliveryType { get; set; }
}

public class PostOfficeForDriver
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}

public class DriverView 
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int DeliveryCount { get; set; }
    public CurrentDriverStatus DriverStatus { get; set; }
    public List<DriverDelivery> Deliveries { get; set; }
}

public class DriverDelivery
{
    public string DeliveryName { get; set; }
    public DeliveryType DeliveryType { get; set; }
}