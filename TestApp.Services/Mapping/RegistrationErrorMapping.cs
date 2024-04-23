namespace TestApp.Services.Mapping;

using Microsoft.AspNetCore.Identity;
using TestApp.Services.Models;

/// <summary>
/// Registration error entity to model mapping.
/// </summary>
public static class RegistrationErrorMapping
{
    /// <summary>
    /// Convert internal ASP.NET identity error to model.
    /// </summary>
    /// <param name="identityError">Internal ASP.NET identity error.</param>
    /// <returns></returns>
    public static RegistrationErrorModel ToModel(this IdentityError identityError)
    {
        return new RegistrationErrorModel(
          identityError.Code,
          identityError.Description);
    }
}
