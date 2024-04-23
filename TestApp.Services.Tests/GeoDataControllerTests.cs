namespace TestApp.Services.Tests;

using Microsoft.AspNetCore.Mvc;
using Moq;
using TestApp.Services.Controllers;
using TestApp.Services.Models;


/// <summary>
/// Contains unit tests for the GeoDataController.
/// </summary>
[TestClass]
public class GeoDataControllerTests
{
    private Mock<IGeoService>? _mockGeoService;
    private GeoDataController? _controller;

    /// <summary>
    /// Initializes resources can be reused across tests in the GeoDataControllerTests class.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        _mockGeoService = new Mock<IGeoService>();
        _controller = new GeoDataController(_mockGeoService.Object);
    }

    /// <summary>
    /// Tests whether the Countries method returns an OkObjectResult with a list of countries.
    /// </summary>
    [TestMethod]
    public async Task Countries_ReturnsOkObjectResultWithCountries()
    {
        // Arrange.
        var fakeCountries = new[]
        {
            new CountryModel(Guid.NewGuid(), "USA"),
            new CountryModel(Guid.NewGuid(), "Canada")
        };

        _mockGeoService!.Setup(service => service.GetCountriesAsync()).ReturnsAsync(fakeCountries);

        // Act.
        var result = await _controller!.Countries();

        // Assert.
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = result as OkObjectResult;

        Assert.IsNotNull(okResult);
        Assert.AreEqual(fakeCountries, okResult.Value);
    }

    /// <summary>
    /// Tests whether the Provinces method returns an OkObjectResult with a list of provinces for a specific country.
    /// </summary>
    [TestMethod]
    public async Task Provinces_ReturnsOkObjectResultWithProvinces()
    {
        // Arrange.
        var countryId = Guid.NewGuid();
        var fakeProvinces = new[]
        {
            new ProvinceModel(Guid.NewGuid(), "California"),
            new ProvinceModel(Guid.NewGuid(), "Quebec")
        };

        _mockGeoService!.Setup(service => service.GetProvincesByCountryAsync(countryId)).ReturnsAsync(fakeProvinces);

        // Act.
        var result = await _controller!.Provinces(countryId);

        // Assert.
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = result as OkObjectResult;

        Assert.IsNotNull(okResult);
        Assert.AreEqual(fakeProvinces, okResult.Value);
    }
}
