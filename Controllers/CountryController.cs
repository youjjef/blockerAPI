using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

[ApiController]
[Route("api/countries")]
public class CountryController : ControllerBase
{
    private readonly ConcurrentDictionary<string, bool> _blockedCountries;

    public CountryController(ConcurrentDictionary<string, bool> blockedCountries)
    {
        _blockedCountries = blockedCountries;
    }

    [HttpPost("block")]
    public IActionResult BlockCountry([FromBody] string countryCode)
    {
        if (!_blockedCountries.TryAdd(countryCode, true))
            return Conflict("Country is already blocked.");
        return Ok($"Blocked {countryCode}");
    }

    [HttpDelete("block/{countryCode}")]
    public IActionResult UnblockCountry(string countryCode)
    {
        if (!_blockedCountries.TryRemove(countryCode, out _))
            return NotFound("Country not found.");
        return Ok($"Unblocked {countryCode}");
    }

    [HttpGet("blocked")]
    public IActionResult GetBlockedCountries()
    {
        return Ok(_blockedCountries.Keys);
    }
}
