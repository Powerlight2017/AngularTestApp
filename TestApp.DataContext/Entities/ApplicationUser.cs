namespace TestApp.DataContext.Entities;

using Microsoft.AspNetCore.Identity;
using TestApp.CommonDataAccess.Entities;

/// <summary>
/// Application user entity.
/// </summary>
public class ApplicationUser : IdentityUser<Guid>, IEntity
{
    /// <summary>
    /// Country Id.
    /// </summary>
    public Guid CountryId { get; set; }

    /// <summary>
    /// Country entity reference.
    /// </summary>
    public Country Country { get; set; } = null!; // by this guide: https://learn.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types
    
    /// <summary>
    /// Province Id.
    /// </summary>
    public Guid ProvinceId { get; set; }

    /// <summary>
    /// Province associated entity.
    /// </summary>
    public Province Province { get; set; } = null!; // by this guide: https://learn.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types
}
