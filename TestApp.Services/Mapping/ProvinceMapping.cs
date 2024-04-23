namespace TestApp.Services.Mapping;

using TestApp.DataContext.Entities;
using TestApp.Services.Models;

/// <summary>
/// Province entitz to model mapping.
/// </summary>
public static class ProvinceMapping
{
    /// <summary>
    /// Convert province to model.
    /// </summary>
    /// <param name="province">Province EF entity.</param>
    /// <returns>Province model.</returns>
    public static ProvinceModel ToModel(this Province province)
    {
        return new ProvinceModel(province.Id, province.Name);
    }
}
