using Microsoft.AspNetCore.Mvc;
using GarageBlazor3.DTOs;
namespace GarageBlazor3.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private static readonly List<VehicleDto> Vehicles =
    [
        new(1, "ABC123", "Volvo"),
        new(2, "XYZ789", "BMW")
    ];

    [HttpGet]
    public IActionResult GetVehicles()
    {
        return Ok(Vehicles);
    }

    [HttpPost]
    public IActionResult AddVehicle([FromBody] CreateVehicleDto request)
    {
        var id = Vehicles.Max(v => v.Id) + 1;

        var vehicle = new VehicleDto(
            id,
            request.RegistrationNumber,
            request.Brand
        );

        Vehicles.Add(vehicle);

        return Ok(vehicle);
    }
}

public record VehicleDto(int Id, string RegistrationNumber, string Brand);

