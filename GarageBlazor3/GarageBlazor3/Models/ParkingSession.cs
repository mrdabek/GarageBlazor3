namespace GarageBlazor3.Models;

public class ParkingSession
{
    public int Id { get; set; }

    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;

    public int ParkingSpotId { get; set; }
    public ParkingSpot ParkingSpot { get; set; } = null!;

    public DateTime CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }

    public decimal RateAtCheckIn { get; set; }
    public decimal? TotalPrice { get; set; }
}   