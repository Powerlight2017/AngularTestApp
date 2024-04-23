namespace TestApp.Services.Models;

/// <summary>
/// Represents an error that can occur during the user registration process.
/// </summary>
public class RegistrationErrorModel
{
    /// <summary>
    /// Gets the error code associated with the registration error.
    /// </summary>
    /// <value>
    /// The code that identifies the type of error.
    /// </value>
    public string Code { get; }

    /// <summary>
    /// Gets the description of the registration error.
    /// </summary>
    /// <value>
    /// The detailed description of what the error is and possibly how to resolve it.
    /// </value>
    public string Description { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RegistrationErrorModel"/> class with a specific error code and description.
    /// </summary>
    /// <param name="code">The error code that identifies the type of registration error.</param>
    /// <param name="description">The detailed description of the registration error.</param>
    public RegistrationErrorModel(string code, string description)
    {
        Code = code;
        Description = description;
    }
}

