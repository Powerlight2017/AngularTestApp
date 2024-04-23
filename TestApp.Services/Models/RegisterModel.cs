namespace TestApp.Services.Models;

/// <summary>
/// Represents the data model for user registration.
/// </summary>
public class RegisterModel
{
    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    /// <value>
    /// The email address used for user registration.
    /// </value>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password for the user.
    /// </summary>
    /// <value>
    /// The password for the user account.
    /// </value>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the country associated with the user.
    /// </summary>
    /// <value>
    /// The unique identifier for the country of the user.
    /// </value>
    public Guid CountryId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the province associated with the user.
    /// </summary>
    /// <value>
    /// The unique identifier for the province of the user.
    /// </value>
    public Guid ProvinceId { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterModel"/> class with specified registration details.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="password">The password for the user account.</param>
    /// <param name="countryId">The unique identifier of the country associated with the user.</param>
    /// <param name="provinceId">The unique identifier of the province associated with the user.</param>
    public RegisterModel(string email, string password, Guid countryId, Guid provinceId)
    {
        Email = email;
        Password = password;
        CountryId = countryId;
        ProvinceId = provinceId;
    }
}
