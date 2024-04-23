namespace TestApp.Services.Controllers;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller to manage geographical data such as countries and provinces.
/// </summary>
[ApiController]
[Route("api/geodata")]
public class GeoDataController : ControllerBase
{
    private readonly IGeoService _geoService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GeoDataController"/> class.
    /// </summary>
    /// <param name="geoService">The geographical data service used by this controller.</param>
    public GeoDataController(IGeoService geoService)
    {
        _geoService = geoService;
    }

    /// <summary>
    /// Gets all countries.
    /// </summary>
    /// <returns>An ActionResult containing a list of all countries.</returns>
    /// <response code="200">Returns the list of countries.</response>
    [HttpGet("countries")]
    public async Task<IActionResult> Countries()
    {
        return Ok(await _geoService.GetCountriesAsync());
    }

    /// <summary>
    /// Gets provinces by country ID.
    /// </summary>
    /// <param name="countryId">The ID of the country for which provinces are requested.</param>
    /// <returns>An ActionResult containing a list of provinces for the specified country.</returns>
    /// <response code="200">Returns the list of provinces for the specified country.</response>
    [HttpGet("provinces/{countryId}")]
    public async Task<IActionResult> Provinces(Guid countryId)
    {
        return Ok(await _geoService.GetProvincesByCountryAsync(countryId));
    }
}
