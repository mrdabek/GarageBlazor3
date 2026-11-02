namespace GarageBlazor3.Models;

public class ParkingSpot
{
    public int Id { get; set; }
    public string Number { get; set; } = "";

    public ICollection<ParkingSession> ParkingSessions { get; set; }
        = new List<ParkingSession>();
}