using System;

namespace PostSystemAPI.Domain.ViewModels;

public class PostOfficeLocation
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}