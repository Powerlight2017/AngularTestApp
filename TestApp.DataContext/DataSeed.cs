using TestApp.DataContext.Entities;

namespace TestApp.DataContext;

/// <summary>
/// Provides methods to seed initial data for Countries and Provinces into the database.
/// </summary>
public static class DataSeed
{
    /// <summary>
    /// Gets a collection of predefined countries.
    /// </summary>
    public static List<Country> Countries { get; } = new List<Country>();

    /// <summary>
    /// Gets a collection of predefined provinces.
    /// </summary>
    public static List<Province> Provinces { get; } = new List<Province>();

    /// <summary>
    /// Initializes static members of the <see cref="DataSeed"/> class by seeding countries and provinces.
    /// </summary>
    static DataSeed()
    {
        SeedCountries();
        SeedProvinces();
    }

    /// <summary>
    /// Seeds initial country data into the Countries list.
    /// </summary>
    private static void SeedCountries()
    {
        Countries.AddRange(new[]
        {
            new Country(new Guid("eb46b6df-bb1e-4b9d-88ff-69996d60cf69"), "United States"),
            new Country(new Guid("8d803248-b936-498d-a954-ec93f50e39c8"), "Canada")
        });
    }

    /// <summary>
    /// Seeds initial province data into the Provinces list.
    /// </summary>
    private static void SeedProvinces()
    {
        Provinces.AddRange(new[]
        {
            new Province(new Guid("c27475ba-28e4-422e-a061-4b8867880b68"), "California", Countries[0].Id),
            new Province(new Guid("f33fcff6-cfe2-4894-a73e-4fa090797672"), "Texas", Countries[0].Id),
            new Province(new Guid("785073b3-869b-4595-a14c-1cb73c240499"), "Ontario", Countries[1].Id)
        });
    }
}
