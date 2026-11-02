using System.ComponentModel.DataAnnotations;

namespace GarageBlazor3.DTOs;

public class CreateVehicleDto
{
    [Required]
    public string RegistrationNumber { get; set; } = "";

    [Required]
    public string Brand { get; set; } = "";
    
    [Range(1, int.MaxValue, ErrorMessage = "Select a vehicle type")]
    public int VehicleTypeId { get; set; }
}