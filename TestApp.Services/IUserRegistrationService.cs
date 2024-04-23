namespace TestApp.Services;

using TestApp.Services.Models;

/// <summary>
/// Defines a contract for a service responsible for handling user registration.
/// </summary>
public interface IUserRegistrationService
{
    /// <summary>
    /// Asynchronously registers a new user based on the provided registration model.
    /// </summary>
    /// <param name="model">The registration details required to create a new user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is a <see cref="RegistrationResultModel"/> 
    /// that contains the outcome of the registration process including success status and any errors encountered.</returns>
    Task<RegistrationResultModel> CreateUserAsync(RegisterModel model);
}
