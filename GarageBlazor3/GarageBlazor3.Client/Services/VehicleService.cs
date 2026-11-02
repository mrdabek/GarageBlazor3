using System.Net.Http.Json;
using GarageBlazor3.Client.DTOs;

namespace GarageBlazor3.Client.Services;

public class VehicleService
{
    private readonly HttpClient _http;

    public VehicleService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<VehicleDto>> GetVehiclesAsync()
    {
        return await _http.GetFromJsonAsync<List<VehicleDto>>("api/vehicles") ?? [];
    }

    public async Task<HttpResponseMessage> AddVehicleAsync(
        string registrationNumber, string brand)
    {
        return await _http.PostAsJsonAsync("api/vehicles", new
        {
            RegistrationNumber = registrationNumber,
            Brand = brand
        });
    }

    public async Task<HttpResponseMessage> UpdateVehicleAsync(
        int id, string registrationNumber, string brand)
    {
        return await _http.PutAsJsonAsync($"api/vehicles/{id}", new
        {
            RegistrationNumber = registrationNumber,
            Brand = brand
        });
    }

    public async Task<HttpResponseMessage> DeleteVehicleAsync(int id)
    {
        return await _http.DeleteAsync($"api/vehicles/{id}");
    }
}