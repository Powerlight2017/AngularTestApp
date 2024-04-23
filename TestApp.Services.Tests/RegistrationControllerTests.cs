namespace TestApp.Services.Tests;

using Microsoft.AspNetCore.Mvc;
using Moq;
using TestApp.Services;
using TestApp.Services.Controllers;
using TestApp.Services.Models;


/// <summary>
/// Contains unit tests for the RegistrationController.
/// </summary>
[TestClass]
public class RegistrationControllerTests
{
    private Mock<IUserRegistrationService>? _mockUserRegistrationService;
    private RegistrationController? _controller;

    /// <summary>
    /// Sets up the necessary mocks and initializes RegistrationController before each test.
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        _mockUserRegistrationService = new Mock<IUserRegistrationService>();
        _controller = new RegistrationController(_mockUserRegistrationService.Object);
    }

    /// <summary>
    /// Tests if Register method returns an OkObjectResult when registration is successful.
    /// </summary>
    [TestMethod]
    public async Task Register_ReturnsOkWhenRegistrationIsSuccessful()
    {
        // Arrange.
        var registerModel = new RegisterModel("test@example.com", "password123", Guid.NewGuid(), Guid.NewGuid());
        var registrationResult = new RegistrationResultModel { Success = true };
        _mockUserRegistrationService!.Setup(service => service.CreateUserAsync(registerModel))
            .ReturnsAsync(registrationResult);

        // Act.
        var result = await _controller!.Register(registerModel);

        // Assert.
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(registrationResult, okResult.Value);
    }

    /// <summary>
    /// Tests if Register method returns a BadRequestObjectResult when registration fails.
    /// </summary>
    [TestMethod]
    public async Task Register_ReturnsBadRequestWhenRegistrationFails()
    {
        // Arrange.
        var registerModel = new RegisterModel("fail@example.com", "password123", Guid.NewGuid(), Guid.NewGuid());
        var registrationResult = new RegistrationResultModel { Success = false };
        registrationResult.Errors.Add(new RegistrationErrorModel("ErrorCode", "Error message."));
        _mockUserRegistrationService!.Setup(service => service.CreateUserAsync(registerModel))
            .ReturnsAsync(registrationResult);

        // Act.
        var result = await _controller!.Register(registerModel);

        // Assert.
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual(400, badRequestResult.StatusCode);
        Assert.AreEqual(registrationResult, badRequestResult.Value);
    }
}
