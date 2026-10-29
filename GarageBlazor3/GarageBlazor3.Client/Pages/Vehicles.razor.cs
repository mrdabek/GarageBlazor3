using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using GarageBlazor3.Client.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GarageBlazor3.Client.Pages;

public partial class Vehicles
{
    [Inject]
    private HttpClient Http { get; set; } = default!;

    [Inject]
    private IJSRuntime JS { get; set; } = default!;

    private List<VehicleDto>? vehicles;
    private CreateVehicleForm newVehicle = new();
    private int? editingVehicleId;

    protected override async Task OnInitializedAsync()
    {
        vehicles = await Http.GetFromJsonAsync<List<VehicleDto>>("api/vehicles");
    }

    private async Task AddVehicle()
    {
        HttpResponseMessage response;

        if (editingVehicleId is not null)
        {
            response = await Http.PutAsJsonAsync(
                $"api/vehicles/{editingVehicleId}",
                newVehicle
            );
        }
        else
        {
            response = await Http.PostAsJsonAsync(
                "api/vehicles",
                newVehicle
            );
        }

        if (response.IsSuccessStatusCode)
        {
            vehicles = await Http.GetFromJsonAsync<List<VehicleDto>>("api/vehicles");
            newVehicle = new CreateVehicleForm();
            editingVehicleId = null;
        }
    }

    private async Task DeleteVehicle(int id)
    {
        var confirmed = await JS.InvokeAsync<bool>(
            "confirm",
            "Are you sure you want to delete this vehicle?"
        );

        if (!confirmed)
            return;

        var response = await Http.DeleteAsync($"api/vehicles/{id}");

        if (response.IsSuccessStatusCode)
        {
            vehicles = await Http.GetFromJsonAsync<List<VehicleDto>>("api/vehicles");
        }
    }

    private void StartEditing(VehicleDto vehicle)
    {
        editingVehicleId = vehicle.Id;

        newVehicle = new CreateVehicleForm
        {
            RegistrationNumber = vehicle.RegistrationNumber,
            Brand = vehicle.Brand
        };
    }

    public class CreateVehicleForm
    {
        [Required]
        public string RegistrationNumber { get; set; } = "";

        [Required]
        public string Brand { get; set; } = "";
    }
}