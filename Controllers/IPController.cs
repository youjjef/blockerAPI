using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

[ApiController]
[Route("api/ip")]
public class IPController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly ApiSettings _apiSettings;

    public IPController(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
    {
        _httpClient = httpClient;
        _apiSettings = apiSettings.Value;
    }

    [HttpGet("lookup")]
    public async Task<IActionResult> LookupIP([FromQuery] string? ipAddress)
    {
        if (string.IsNullOrEmpty(ipAddress))
        {
            return BadRequest("IP address is required.");
        }

        string apiUrl = $"https://ipapi.co/{ipAddress}/json/?key={_apiSettings.IPGeolocationAPIKey}";
        var response = await _httpClient.GetStringAsync(apiUrl);
        var data = JsonSerializer.Deserialize<Dictionary<string, string>>(response);
        if (data == null)
        {
            return NotFound("IP address data not found.");
        }

        return Ok(new 
        { 
            CountryCode = data.ContainsKey("country_code") ? data["country_code"] : null, 
            CountryName = data.ContainsKey("country_name") ? data["country_name"] : null, 
            Organization = data.ContainsKey("org") ? data["org"] : null 
        });
    }
}