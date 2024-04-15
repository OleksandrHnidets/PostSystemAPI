using System;
using PostSystemAPI.DAL.Enums;

namespace PostSystemAPI.Domain.ViewModels;

public class DeliveriesViewModel
{
    public string Id { get; set; }
    public string DeliveryName { get; set; }
    public string DeliveryDescription { get; set; }
    public DateTime DeliveryDate { get; set; }
    public int Price { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
    public DeliveryType DeliveryType { get; set; }
    public bool IsFinished { get; set; }
    public string SentUser { get; set; }
    public string ReceivedUser { get; set; }
}