namespace TestApp.DataContext.Entities;

using TestApp.CommonDataAccess.Entities;

/// <summary>
/// Country entity.
/// </summary>
public class Country : Entity
{
    /// <summary>
    /// Country name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Country provinces collection.
    /// </summary>
    public ICollection<Province> Provinces { get; set; } = null!; // by this guide: https://learn.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types

    /// <summary>
    /// Country entity constructor.
    /// </summary>
    /// <param name="id">Id.</param>
    /// <param name="name">Name.</param>
    public Country(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}