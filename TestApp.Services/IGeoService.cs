namespace TestApp.Services;

using TestApp.Services.Models;

/// <summary>
/// Defines the contract for services that provide access to geographic data, including countries and provinces.
/// </summary>
public interface IGeoService
{
    /// <summary>
    /// Asynchronously retrieves a list of all countries.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an array of <see cref="CountryModel"/>, representing all countries.</returns>
    Task<CountryModel[]> GetCountriesAsync();

    /// <summary>
    /// Asynchronously retrieves a list of provinces for a specified country.
    /// </summary>
    /// <param name="countryId">The unique identifier of the country for which to retrieve provinces.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an array of <see cref="ProvinceModel"/>, representing the provinces of the specified country.</returns>
    Task<ProvinceModel[]> GetProvincesByCountryAsync(Guid countryId);
}
