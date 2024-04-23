namespace TestApp.Services.Models;

/// <summary>
/// Represents the result of a registration operation.
/// </summary>
public class RegistrationResultModel
{
    /// <summary>
    /// Gets or sets a value indicating whether the registration was successful.
    /// </summary>
    /// <value>
    /// True if registration was successful; otherwise, false.
    /// </value>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the list of errors that occurred during the registration process.
    /// </summary>
    /// <value>
    /// A list of <see cref="RegistrationErrorModel"/> instances containing error details.
    /// </value>
    public List<RegistrationErrorModel> Errors { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RegistrationResultModel"/> class.
    /// </summary>
    public RegistrationResultModel()
    {
        Errors = new List<RegistrationErrorModel>();
    }
}
