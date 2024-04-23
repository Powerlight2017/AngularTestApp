namespace TestApp.CommonDataAccess.Entities;

/// <summary>
/// Common entity interface.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Entity id.
    /// </summary>
    public Guid Id { get; set; }
}
