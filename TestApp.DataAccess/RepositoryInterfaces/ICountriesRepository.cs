namespace TestApp.DataAccess.RepositoryInterfaces;

using TestApp.CommonDataAccess.RepositoryInterfaces;
using TestApp.DataContext.Entities;

/// <summary>
/// Countries repository.
/// </summary>
public interface ICountriesRepository : IRepositoryBase<Country>
{
}
