namespace TestApp.Services.Controllers;

using Microsoft.AspNetCore.Mvc;
using TestApp.Services;
using TestApp.Services.Models;

/// <summary>
/// Handles user registration requests.
/// </summary>
[ApiController]
[Route("api/registration")]
public class RegistrationController : ControllerBase
{
    private readonly IUserRegistrationService userRegistrationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegistrationController"/> class.
    /// </summary>
    /// <param name="userRegistrationService">The service to handle user registration logic.</param>
    public RegistrationController(IUserRegistrationService userRegistrationService)
    {
        this.userRegistrationService = userRegistrationService;
    }

    /// <summary>
    /// Registers a new user with the provided user details.
    /// </summary>
    /// <param name="model">The registration details required to register a new user.</param>
    /// <returns>A response that indicates whether the registration was successful or not.</returns>
    /// <remarks>
    /// POST: registration/register
    /// Sample request:
    /// 
    ///     POST /registration/register
    ///     {
    ///        "username": "some_user",
    ///        "email": "some@email.com",
    ///        "password": "strongPassword123!"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">If the user was successfully registered.</response>
    /// <response code="400">If the registration details are invalid/user already exists.</response>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        RegistrationResultModel result = await userRegistrationService.CreateUserAsync(model);

        if (result.Success)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }
}
