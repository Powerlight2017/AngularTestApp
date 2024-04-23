namespace TestApp.Services.Tests;

using Microsoft.AspNetCore.Identity;
using Moq;
using TestApp.DataAccess.RepositoryInterfaces;
using TestApp.Services.Models;
using TestApp.Services;
using TestApp.DataContext.Entities;

/// <summary>
/// Contains unit tests for the UserRegistrationService class to verify user registration logic.
/// </summary>
[TestClass]
public class UserRegistrationServiceTests
{
    private Mock<ICountriesRepository>? _mockCountryRepository;
    private Mock<IProvincesRepository>? _mockProvinceRepository;
    private Mock<FakeUserManager>? _mockUserManager;
    private UserRegistrationService? _service;

    /// <summary>
    /// Sets up mocks and initializes the UserRegistrationService before each test.
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        _mockCountryRepository = new Mock<ICountriesRepository>();
        _mockProvinceRepository = new Mock<IProvincesRepository>();
        _mockUserManager = new Mock<FakeUserManager>();

        _service = new UserRegistrationService(_mockCountryRepository.Object, _mockProvinceRepository.Object, _mockUserManager.Object);
    }

    /// <summary>
    /// Tests that CreateUserAsync returns an error result when the specified country is not found.
    /// </summary>
    [TestMethod]
    public async Task CreateUserAsync_CountryNotFound_ReturnsError()
    {
        // Arrange.
        var model = new RegisterModel("test@example.com", "Password123!", Guid.NewGuid(), Guid.NewGuid());
        _mockCountryRepository!.Setup(x => x.GetByIdAsync(model.CountryId)).ReturnsAsync((Country?)null);

        // Act.
        var result = await _service!.CreateUserAsync(model);

        // Assert.
        Assert.IsFalse(result.Success);
        Assert.IsTrue(result.Errors.Any(e => e.Code == "CountryNotFound"));
    }

    /// <summary>
    /// Tests that CreateUserAsync returns an error result when the specified province is not found.
    /// </summary>
    [TestMethod]
    public async Task CreateUserAsync_ProvinceNotFound_ReturnsError()
    {
        // Arrange.
        var countryId = Guid.NewGuid();
        var model = new RegisterModel("test@example.com", "Password123!", countryId, Guid.NewGuid());
        _mockCountryRepository!.Setup(x => x.GetByIdAsync(countryId)).ReturnsAsync(new Country(countryId, "USA"));
        _mockProvinceRepository!.Setup(x => x.GetByIdAsync(model.ProvinceId)).ReturnsAsync((Province?)null);

        // Act.
        var result = await _service!.CreateUserAsync(model);

        // Assert.
        Assert.IsFalse(result.Success);
        Assert.IsTrue(result.Errors.Any(e => e.Code == "ProvinceNotFound"));
    }

    /// <summary>
    /// Tests that CreateUserAsync successfully creates a user when all conditions are met.
    /// </summary>
    [TestMethod]
    public async Task CreateUserAsync_SuccessfulCreation_ReturnsSuccess()
    {
        // Arrange.
        var countryId = Guid.NewGuid();
        var provinceId = Guid.NewGuid();
        var model = new RegisterModel("test@example.com", "StrongPassword123!", countryId, provinceId);
        _mockCountryRepository!.Setup(x => x.GetByIdAsync(countryId)).ReturnsAsync(new Country(countryId, "USA"));
        _mockProvinceRepository!.Setup(x => x.GetByIdAsync(provinceId)).ReturnsAsync(new Province(provinceId, "California", countryId));
        _mockUserManager!.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), model.Password)).ReturnsAsync(IdentityResult.Success);

        // Act.
        var result = await _service!.CreateUserAsync(model);

        // Assert.
        Assert.IsTrue(result.Success);
        Assert.AreEqual(0, result.Errors.Count);
    }

    /// <summary>
    /// Tests that CreateUserAsync returns a failure result when UserManager reports errors.
    /// </summary>
    [TestMethod]
    public async Task CreateUserAsync_UserManagerReturnsErrors_ReturnsFailure()
    {
        // Arrange.
        var countryId = Guid.NewGuid();
        var provinceId = Guid.NewGuid();
        var model = new RegisterModel("test@example.com", "WeakPassword", countryId, provinceId);
        var errors = new List<IdentityError>
        {
            new IdentityError { Code = "WeakPassword", Description = "Password is too weak." }
        };
        _mockCountryRepository!.Setup(x => x.GetByIdAsync(countryId)).ReturnsAsync(new Country(countryId, "USA"));
        _mockProvinceRepository!.Setup(x => x.GetByIdAsync(provinceId)).ReturnsAsync(new Province(provinceId, "California", countryId));
        _mockUserManager!.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), model.Password))
                        .ReturnsAsync(IdentityResult.Failed(errors.ToArray()));

        // Act.
        var result = await _service!.CreateUserAsync(model);

        // Assert.
        Assert.IsFalse(result.Success);
        Assert.IsTrue(result.Errors.Any(e => e.Code == "WeakPassword"));
    }
}