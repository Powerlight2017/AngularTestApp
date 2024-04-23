namespace TestApp.DataAccess.Repositories;

using Microsoft.EntityFrameworkCore;
using TestApp.CommonDataAccess.Repositories;
using TestApp.DataAccess.RepositoryInterfaces;
using TestApp.DataContext.Entities;

/// <summary>
/// Countries repository.
/// </summary>
public class CountriesRepository : RepositoryBase<Country>, ICountriesRepository
{
    /// <summary>
    /// Countries repository constructor.
    /// </summary>
    /// <param name="context">DB context.</param>
    public CountriesRepository(DbContext context) : base(context)
    {
    }
}

