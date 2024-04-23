namespace TestApp.Services;

using Microsoft.EntityFrameworkCore;
using TestApp.DataAccess.RepositoryInterfaces;
using TestApp.Services.Mapping;
using TestApp.Services.Models;


/// <summary>
/// Provides services for accessing geographic data such as countries and provinces.
/// </summary>
public class GeoService : IGeoService
{
    private readonly ICountriesRepository _countryRepository;
    private readonly IProvincesRepository _provinceRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GeoService"/> class.
    /// </summary>
    /// <param name="countryRepository">The repository for accessing country data.</param>
    /// <param name="provinceRepository">The repository for accessing province data.</param>
    public GeoService(ICountriesRepository countryRepository, IProvincesRepository provinceRepository)
    {
        _countryRepository = countryRepository;
        _provinceRepository = provinceRepository;
    }

    /// <summary>
    /// Asynchronously retrieves all countries from the repository and maps them to country models.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an array of <see cref="CountryModel"/>.</returns>
    public async Task<CountryModel[]> GetCountriesAsync()
    {
        return await _countryRepository.GetAll()
            .Select(x => x.ToModel()).ToArrayAsync();
    }

    /// <summary>
    /// Asynchronously retrieves all provinces for a specified country from the repository and maps them to province models.
    /// </summary>
    /// <param name="countryId">The unique identifier of the country for which provinces are requested.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an array of <see cref="ProvinceModel"/>.</returns>
    public async Task<ProvinceModel[]> GetProvincesByCountryAsync(Guid countryId)
    {
        return await _provinceRepository.GetAll()
            .Where(x => x.CountryId == countryId)
            .Select(x => x.ToModel()).ToArrayAsync();
    }
}
