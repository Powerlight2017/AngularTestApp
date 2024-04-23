namespace TestApp.Services.Mapping;

using TestApp.DataContext.Entities;
using TestApp.Services.Models;

/// <summary>
/// Country entity to model mapping.
/// </summary>
public static class CountryMapping
{
    /// <summary>
    /// Convert current entity to model.
    /// </summary>
    /// <param name="country">Country EF entity.</param>
    /// <returns>Country model.</returns>
    public static CountryModel ToModel(this Country country)
    {
        return new CountryModel(country.Id, country.Name);
    }
}
