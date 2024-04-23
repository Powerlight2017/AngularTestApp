namespace TestApp.Services.Models;

/// <summary>
/// Represents a country with identifiable properties.
/// </summary>
public class CountryModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the country.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the country.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CountryModel"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the country.</param>
    /// <param name="name">The name of the country.</param>
    public CountryModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
