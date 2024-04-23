namespace TestApp.Services.Models;

/// <summary>
/// Represents a province with identifiable properties.
/// </summary>
public class ProvinceModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the province.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the province.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the country to which this province belongs.
    /// </summary>
    public Guid CountryId { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProvinceModel"/> class with specified details.
    /// </summary>
    /// <param name="id">The unique identifier of the province.</param>
    /// <param name="name">The name of the province.</param>
    public ProvinceModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
