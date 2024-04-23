namespace TestApp.Services.Tests;

using MockQueryable.Moq;
using Moq;
using TestApp.DataAccess.RepositoryInterfaces;
using TestApp.DataContext.Entities;

/// <summary>
/// Contains unit tests for the GeoService class which provides geographical data services.
/// </summary>
[TestClass]
public class GeoServiceTests
{
    /// <summary>
    /// Mocked countries repository.
    /// </summary>
    private Mock<ICountriesRepository>? _mockCountryRepository;

    /// <summary>
    /// Mocked provinces repository.
    /// </summary>
    private Mock<IProvincesRepository>? _mockProvinceRepository;
    
    /// <summary>
    /// Geo service for test.
    /// </summary>
    private GeoService? _geoService;

    private readonly Guid USACountryId = new Guid("2b70a70c-a253-402c-8eb4-1acf0451e389");
    private readonly Guid CanadaCountryId = new Guid("1820d99e-bef2-4b8b-8177-7a7708b08630");

    /// <summary>
    /// Sets up the necessary mocks and initializes GeoService before each test.
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        _mockCountryRepository = new Mock<ICountriesRepository>();
        _mockProvinceRepository = new Mock<IProvincesRepository>();

        _mockCountryRepository.Setup(x => x.GetAll()).Returns(GetMockCountries());
        _mockProvinceRepository.Setup(x => x.GetAll()).Returns(GetMockProvinces());

        _geoService = new GeoService(_mockCountryRepository.Object, _mockProvinceRepository.Object);
    }

    /// <summary>
    /// Tests if GetCountriesAsync correctly retrieves and returns all countries.
    /// </summary>
    [TestMethod]
    public async Task GetCountriesAsync_ReturnsCountries()
    {
        // Arrange
        // Act
        var result = await _geoService!.GetCountriesAsync();

        // Assert
        Assert.AreEqual(2, result.Length);
        Assert.IsTrue(result.Any(x => x.Name == "USA"));
        Assert.IsTrue(result.Any(x => x.Name == "Canada"));
    }

    /// <summary>
    /// Tests if GetProvincesByCountryAsync correctly retrieves and returns provinces for a specific country.
    /// </summary>
    [TestMethod]
    public async Task GetProvincesByCountryAsync_ReturnsProvinces()
    {
        // Arrange
        // Act
        var resultForUsa = await _geoService!.GetProvincesByCountryAsync(USACountryId);
        var resultForCanada = await _geoService!.GetProvincesByCountryAsync(CanadaCountryId);

        // Assert
        Assert.AreEqual(1, resultForUsa.Length);
        Assert.AreEqual(1, resultForCanada.Length);
        Assert.IsTrue(resultForUsa.Any(x => x.Name == "California"));
        Assert.IsTrue(resultForCanada.Any(x => x.Name == "Quebec"));
    }

    /// <summary>
    /// Provides mock country data for testing.
    /// </summary>
    /// <returns>An IQueryable of Country objects.</returns>
    private IQueryable<Country> GetMockCountries()
    {
        var countries = new List<Country>
        {
            new Country(USACountryId, "USA"),
            new Country(CanadaCountryId, "Canada")
        };

        return countries.AsQueryable().BuildMock();
    }

    /// <summary>
    /// Provides mock province data for testing.
    /// </summary>
    /// <returns>An IQueryable of Province objects.</returns>
    private IQueryable<Province> GetMockProvinces()
    {
        var provinces = new List<Province>
        {
            new Province(Guid.NewGuid(), "California", USACountryId),
            new Province(Guid.NewGuid(), "Quebec", CanadaCountryId)
        };

        return provinces.AsQueryable().BuildMock();
    }
}
