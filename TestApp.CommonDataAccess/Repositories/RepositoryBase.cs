namespace TestApp.CommonDataAccess.Repositories;

using Microsoft.EntityFrameworkCore;
using TestApp.CommonDataAccess.Entities;
using TestApp.CommonDataAccess.RepositoryInterfaces;

/// <summary>
/// Provides a generic repository pattern implementation for entity operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity this repository manages. TEntity must implement IEntity.</typeparam>
public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// DB context.
    /// </summary>
    private readonly DbContext _context;

    /// <summary>
    /// Data set for TEntity type.
    /// </summary>
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Initializes a new instance of the RepositoryBase class.
    /// </summary>
    /// <param name="context">The database context to be used for data operations.</param>
    public RepositoryBase(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    /// <summary>
    /// Retrieves all entities of type TEntity.
    /// </summary>
    /// <returns>An IQueryable of TEntity that can be used to enumerate over the entities.</returns>
    public IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsNoTracking();
    }

    /// <summary>
    /// Asynchronously finds an entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to find.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the found entity or null if no entity is found.</returns>
    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Asynchronously adds a new entity to the database.
    /// </summary>
    /// <param name="item">The entity to add.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    public async Task<int> CreateAsync(TEntity item)
    {
        await _dbSet.AddAsync(item);
        return await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously updates an existing entity in the database.
    /// </summary>
    /// <param name="item">The entity to update.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    public async Task<int> UpdateAsync(TEntity item)
    {
        _context.Entry(item).State = EntityState.Modified;
        return await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously removes an entity from the database by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to remove.</param>
    /// <returns>A task that represents the asynchronous remove operation. The task result contains the number of state entries written to the database.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if no entity with the given ID is found.</exception>
    public async Task<int> RemoveAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException("Entity with specified ID not found.");
        }

        _dbSet.Remove(entity);
        return await _context.SaveChangesAsync();
    }
}
