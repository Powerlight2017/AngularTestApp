namespace TestApp.DataAccess.Repositories;

using Microsoft.EntityFrameworkCore;
using TestApp.CommonDataAccess.Repositories;
using TestApp.DataAccess.RepositoryInterfaces;
using TestApp.DataContext.Entities;


/// <summary>
/// Provinces repository.
/// </summary>
public class ProvincesRepository : RepositoryBase<Province>, IProvincesRepository
{
    /// <summary>
    /// Provinces repository.
    /// </summary>
    /// <param name="context">Database context.</param>
    public ProvincesRepository(DbContext context) : base(context)
    {
    }
}
