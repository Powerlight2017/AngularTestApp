namespace TestApp.Services;

using Microsoft.AspNetCore.Identity;
using TestApp.DataAccess.RepositoryInterfaces;
using TestApp.DataContext.Entities;
using TestApp.Services.Mapping;
using TestApp.Services.Models;

/// <summary>
/// Service handling the registration of new users, including validation of geographical data.
/// </summary>
public class UserRegistrationService : IUserRegistrationService
{
    /// <summary>
    /// Countries repository.
    /// </summary>
    private readonly ICountriesRepository _countryRepository;

    /// <summary>
    /// Provinces repository.
    /// </summary>
    private readonly IProvincesRepository _provinceRepository;

    /// <summary>
    /// Microsoft ASP.NET identity user manager.
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRegistrationService"/> class.
    /// </summary>
    /// <param name="countryRepository">Repository for accessing country data.</param>
    /// <param name="provinceRepository">Repository for accessing province data.</param>
    /// <param name="userManager">Manager for handling user-related operations in ASP.NET Identity.</param>
    public UserRegistrationService(ICountriesRepository countryRepository, IProvincesRepository provinceRepository,
        UserManager<ApplicationUser> userManager)
    {
        _countryRepository = countryRepository;
        _provinceRepository = provinceRepository;
        _userManager = userManager;
    }

    /// <summary>
    /// Asynchronously registers a new user based on the provided registration model.
    /// </summary>
    /// <param name="model">The registration model containing user and location details.</param>
    /// <returns>A task that represents the asynchronous operation, containing the registration result including success status and any errors.</returns>
    public async Task<RegistrationResultModel> CreateUserAsync(RegisterModel model)
    {
        var modelResult = new RegistrationResultModel();

        var country = await _countryRepository.GetByIdAsync(model.CountryId);
        var province = await _provinceRepository.GetByIdAsync(model.ProvinceId);

        // Handle missing country or province by adding errors to the result model.
        if (country == null)
        {
            modelResult.Errors.Add(new RegistrationErrorModel("CountryNotFound", "Country with supplied Id is not found."));
        }

        if (province == null)
        {
            modelResult.Errors.Add(new RegistrationErrorModel("ProvinceNotFound", "Province with supplied Id is not found."));
        }

        // Do not proceed with registration if either country or province is not found.
        if (province == null || country == null)
        {
            return modelResult;
        }

        // Create new user with the specified details.
        var user = new ApplicationUser { UserName = model.Email, CountryId = country.Id, ProvinceId = province.Id };
        var result = await _userManager.CreateAsync(user, model.Password);

        // Populate the registration result model based on the success or failure of user creation.
        modelResult.Success = result.Succeeded;
        modelResult.Errors = result.Errors.Select(x => x.ToModel()).ToList();

        return modelResult;
    }
}
