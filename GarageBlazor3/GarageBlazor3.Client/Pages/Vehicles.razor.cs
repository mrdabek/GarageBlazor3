using System.ComponentModel.DataAnnotations;
using GarageBlazor3.Client.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using GarageBlazor3.Client.Services;

namespace GarageBlazor3.Client.Pages;

public partial class Vehicles
{
    [Inject]
    private VehicleService VehicleService { get; set; } = default!;

    [Inject]
    private IJSRuntime JS { get; set; } = default!;

    private List<VehicleDto>? vehicles;
    private List<VehicleTypeDto> vehicleTypes = [];
    private CreateVehicleForm newVehicle = new();
    private int? editingVehicleId;

    protected override async Task OnInitializedAsync()
    {
        vehicles = await VehicleService.GetVehiclesAsync();
        vehicleTypes = await VehicleService.GetVehicleTypesAsync();
    }

    private async Task AddVehicle()
    {
        HttpResponseMessage response;

        if (editingVehicleId is not null)
        {
            // Edycja istniejącego pojazdu
            response = await VehicleService.UpdateVehicleAsync(
                editingVehicleId.Value,
                newVehicle.RegistrationNumber,
                newVehicle.Brand,
                newVehicle.VehicleTypeId);
        }
        else
        {
            // Dodawanie nowego pojazdu
            response = await VehicleService.AddVehicleAsync(
                newVehicle.RegistrationNumber,
                newVehicle.Brand,
                newVehicle.VehicleTypeId);
        }

        if (response.IsSuccessStatusCode)
        {
            vehicles = await VehicleService.GetVehiclesAsync();
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

        var response = await VehicleService.DeleteVehicleAsync(id);

        if (response.IsSuccessStatusCode)
        {
            vehicles = await VehicleService.GetVehiclesAsync();
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
        
        [Range(1, int.MaxValue, ErrorMessage = "Select a vehicle type")]
        public int VehicleTypeId { get; set; }
    }
}