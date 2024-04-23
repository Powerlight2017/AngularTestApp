namespace TestApp.DataAccess.Tests;

using Microsoft.EntityFrameworkCore;
using TestApp.CommonDataAccess.Repositories;
using TestApp.DataContext;
using TestApp.DataContext.Entities;

/// <summary>
/// Provides unit tests for the RepositoryBase<T> with Country entities.
/// </summary>
[TestClass]
public class RepositoryBaseTests
{
    private ApplicationDbContext? _dbContext;
    private RepositoryBase<Country>? _repository;

    /// <summary>
    /// Initializes resources for each test by setting up an in-memory database context.
    /// </summary>
    [TestInitialize]
    public void TestInitialize()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _dbContext.Database.OpenConnection();
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
        _repository = new RepositoryBase<Country>(_dbContext);
    }

    /// <summary>
    /// Cleans up resources after each test by deleting the in-memory database and closing the connection.
    /// </summary>
    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext!.Database.EnsureDeleted();
        _dbContext.Database.CloseConnection();
        _dbContext.Dispose();
    }

    /// <summary>
    /// Tests whether GetAll method retrieves all entities from the database context.
    /// </summary>
    [TestMethod]
    public async Task GetAll_ReturnsEntities()
    {
        var entitiesFromRepository = await _repository!.GetAll().ToListAsync();
        var entitiesFromContext = await _dbContext!.Set<Country>().ToListAsync();

        Assert.IsNotNull(entitiesFromRepository);
        Assert.AreEqual(entitiesFromContext.Count, entitiesFromRepository.Count);
    }

    /// <summary>
    /// Tests whether CreateAsync method successfully adds an entity to the context.
    /// </summary>
    [TestMethod]
    public async Task CreateAsync_AddsEntityToContext()
    {
        var country = new Country(Guid.NewGuid(), "Create Test");

        await _repository!.CreateAsync(country);
        var retrievedEntity = await _dbContext!.Set<Country>().FindAsync(country.Id);

        Assert.IsNotNull(retrievedEntity);
        Assert.AreEqual("Create Test", retrievedEntity.Name);
    }

    /// <summary>
    /// Tests whether GetByIdAsync correctly retrieves an entity based on its ID.
    /// </summary>
    [TestMethod]
    public async Task GetByIdAsync_ReturnsEntity()
    {
        var country = new Country(Guid.NewGuid(), "Read Test");
        await _dbContext!.Set<Country>().AddAsync(country);
        await _dbContext.SaveChangesAsync();

        var retrievedEntity = await _repository!.GetByIdAsync(country.Id);

        Assert.IsNotNull(retrievedEntity);
        Assert.AreEqual("Read Test", retrievedEntity.Name);
    }

    /// <summary>
    /// Tests whether UpdateAsync correctly updates an entity in the database.
    /// </summary>
    [TestMethod]
    public async Task UpdateAsync_UpdatesEntity()
    {
        var country = new Country(Guid.NewGuid(), "Before update");
        await _dbContext!.Set<Country>().AddAsync(country);
        await _dbContext.SaveChangesAsync();

        country.Name = "After Update";
        await _repository!.UpdateAsync(country);
        var updatedEntity = await _dbContext.Set<Country>().FindAsync(country.Id);

        Assert.IsNotNull(updatedEntity);
        Assert.AreEqual("After Update", updatedEntity.Name);
    }

    /// <summary>
    /// Tests whether RemoveAsync successfully removes an entity from the database.
    /// </summary>
    [TestMethod]
    public async Task RemoveAsync_RemovesEntity()
    {
        var country = new Country(Guid.NewGuid(), "Delete test");
        await _dbContext!.Set<Country>().AddAsync(country);
        await _dbContext!.SaveChangesAsync();

        await _repository!.RemoveAsync(country.Id);
        var deletedEntity = await _dbContext.Set<Country>().FindAsync(country.Id);

        Assert.IsNull(deletedEntity);
    }
}
