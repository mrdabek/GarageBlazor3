using System.ComponentModel.DataAnnotations;

namespace GarageBlazor3.DTOs;

public class CreateVehicleDto
{
    [Required]
    public string RegistrationNumber { get; set; } = "";

    [Required]
    public string Brand { get; set; } = "";
}