using GarageBlazor3.Data;

namespace GarageBlazor3.Models;

public class Vehicle
{
    public int Id { get; set; }

    public string RegistrationNumber { get; set; } = "";
    public string Color { get; set; } = "";
    public string Brand { get; set; } = "";
    public string Model { get; set; } = "";
    public int NumberOfWheels { get; set; }

    public int VehicleTypeId { get; set; }
    public VehicleType VehicleType { get; set; } = null!;

    public string OwnerId { get; set; } = "";
    public ApplicationUser Owner { get; set; } = null!;
}