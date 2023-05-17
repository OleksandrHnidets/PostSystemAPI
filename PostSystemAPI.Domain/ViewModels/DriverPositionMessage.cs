namespace PostSystemAPI.Domain.ViewModels;

public class DriverPositionMessage
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string DriverId { get; set; }
    public string DeliveryId { get; set; }
}