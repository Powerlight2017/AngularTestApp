namespace TestApp.CommonDataAccess.RepositoryInterfaces;

using TestApp.CommonDataAccess.Entities;

/// <summary>
/// Defines a generic repository for managing entities in a database.
/// </summary>
/// <typeparam name="TEntity">The entity type managed by the repository. This type must implement IEntity.</typeparam>
public interface IRepositoryBase<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Retrieves all entities of type TEntity from the database.
    /// </summary>
    /// <returns>An IQueryable of TEntity that can be used to filter and project sequences of entities asynchronously.</returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Asynchronously finds an entity with the given ID.
    /// </summary>
    /// <param name="id">The unique identifier for the entity to be found.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the found entity or null if no entity is found.</returns>
    Task<TEntity?> GetByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously adds a new entity to the database.
    /// </summary>
    /// <param name="item">The new entity to add.</param>
    /// <returns>A task that represents the asynchronous insert operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> CreateAsync(TEntity item);

    /// <summary>
    /// Asynchronously updates an existing entity in the database.
    /// </summary>
    /// <param name="item">The entity to update.</param>
    /// <returns>A task that represents the asynchronous update operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> UpdateAsync(TEntity item);

    /// <summary>
    /// Asynchronously removes an entity from the database using its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to be removed.</param>
    /// <returns>A task that represents the asynchronous remove operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> RemoveAsync(Guid id);
}
