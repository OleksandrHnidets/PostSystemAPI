using PostSystemAPI.DAL.Enums;

namespace PostSystemAPI.Domain.ViewModels;

public class CreateDeliveryViemModel
{
    public string DeliveryName { get; set; }
    public string DeliveryDescription { get; set; }
    public int Price { get; set; }
    public DeliveryType DeliveryType { get; set; }
    public string SenderName { get; set; }
    public string ReceiverName { get; set; }
    public string FromId { get; set; }
    public string ToId { get; set; }
}

public class UpdateDeliveryViewModel
{
    public string Id { get; set; }
    public string DeliveryName { get; set; }
    public string DeliveryDescription { get; set; }
    public int Price { get; set; }
    public DeliveryType DeliveryType { get; set; }
    public string ReceiverName { get; set; }
    public string FromId { get; set; }
    public string FromName { get; set; }
    public string ToId { get; set; }
    public string ToName { get; set; }
}