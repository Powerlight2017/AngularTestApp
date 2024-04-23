namespace TestApp.DataContext.Entities;

using TestApp.CommonDataAccess.Entities;

/// <summary>
/// Province entity.
/// </summary>
public class Province : Entity
{
    /// <summary>
    /// Associated country Id for province.
    /// </summary>
    public Guid CountryId { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Associated country entity reference for province.
    /// </summary>
    public Country Country { get; set; } = null!; // by this guide: https://learn.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="countryId"></param>
    public Province(Guid id, string name, Guid countryId)
    {
        Id = id;
        Name = name;
        CountryId = countryId;
    }
}