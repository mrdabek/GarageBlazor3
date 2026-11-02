using Microsoft.AspNetCore.Mvc;
using GarageBlazor3.DTOs;
using GarageBlazor3.Data;
using GarageBlazor3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace GarageBlazor3.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public VehiclesController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    private readonly UserManager<ApplicationUser> _userManager;

    [HttpGet]
    public async Task<IActionResult> GetVehicles()
    {
        var vehicles = await _context.Vehicles
            .Select(v => new VehicleDto(
                v.Id,
                v.RegistrationNumber,
                v.Brand))
            .ToListAsync();

        return Ok(vehicles);
    }
    
    [HttpGet("types")]
    public async Task<IActionResult> GetVehicleTypes()
    {
        var types = await _context.VehicleTypes
            .OrderBy(t => t.Name)
            .Select(t => new { t.Id, t.Name })
            .ToListAsync();

        return Ok(types);
    }

    [HttpPost]
    public async Task<IActionResult> AddVehicle(
        [FromBody] CreateVehicleDto request)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return Unauthorized();
        }
        
        var vehicleTypeExists = await _context.VehicleTypes
            .AnyAsync(t => t.Id == request.VehicleTypeId);

        if (!vehicleTypeExists)
        {
            return BadRequest("Invalid vehicle type.");
        }
        
        var vehicle = new Vehicle
        {
            RegistrationNumber = request.RegistrationNumber,
            Brand = request.Brand,
            VehicleTypeId = request.VehicleTypeId,
            OwnerId = user.Id
            
        };

        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();

        return Ok(new VehicleDto(
            vehicle.Id,
            vehicle.RegistrationNumber,
            vehicle.Brand
        ));
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);

        if (vehicle is null)
        {
            return NotFound();
        }

        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateVehicle(
        int id, [FromBody] CreateVehicleDto request)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);

        if (vehicle is null)
        {
            return NotFound();
        }

        var vehicleTypeExists = await _context.VehicleTypes
            .AnyAsync(t => t.Id == request.VehicleTypeId);

        if (!vehicleTypeExists)
        {
            return BadRequest("Invalid vehicle type.");
        }

        vehicle.RegistrationNumber = request.RegistrationNumber;
        vehicle.Brand = request.Brand;
        vehicle.VehicleTypeId = request.VehicleTypeId;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}

public record VehicleDto(int Id, string RegistrationNumber, string Brand);

