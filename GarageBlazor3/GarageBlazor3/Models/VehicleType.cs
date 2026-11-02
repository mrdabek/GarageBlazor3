namespace GarageBlazor3.Models;

public class VehicleType
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}