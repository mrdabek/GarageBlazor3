// using Microsoft.AspNetCore.Mvc;
//
// namespace GarageBlazor3.Controllers;
//
// public class VehiclesController : Controller
// {
//     // GET
//     public IActionResult Index()
//     {
//         return View();
//     }
// }

using Microsoft.AspNetCore.Mvc;

namespace GarageBlazor3.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetVehicles()
    {
        var vehicles = new[]
        {
            new { Id = 1, RegistrationNumber = "ABC123", Brand = "Volvo" },
            new { Id = 2, RegistrationNumber = "XYZ789", Brand = "BMW" }
        };

        return Ok(vehicles);
    }
}