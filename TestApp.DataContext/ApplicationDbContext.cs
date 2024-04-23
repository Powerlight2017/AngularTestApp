namespace TestApp.DataContext;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestApp.DataContext.Entities;

/// <summary>
/// The application database context that integrates ASP.NET Identity and includes custom entity definitions.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the database set for Countries.
    /// </summary>
    public DbSet<Country> Countries { get; set; }

    /// <summary>
    /// Gets or sets the database set for Provinces.
    /// </summary>
    public DbSet<Province> Provinces { get; set; }

    /// <summary>
    /// Configures the schema needed for the identity system in the application,
    /// and configures other entity relationships and seeding.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ensure ASP.NET Identity model is configured.
        base.OnModelCreating(modelBuilder);

        // Seed data for the Country entity.
        modelBuilder.Entity<Country>()
            .HasData(DataSeed.Countries);

        // Configure relationship and foreign key for Provinces referring back to Countries.
        modelBuilder.Entity<Province>()
            .HasOne(c => c.Country)
            .WithMany(p => p.Provinces)
            .HasForeignKey(c => c.CountryId);

        // Prevent cascade deletion of Users when a Country or Province is deleted.
        // This is to ensure registered users are not inadvertently removed if referenced locations are deleted.
        modelBuilder.Entity<ApplicationUser>()
                .HasOne(e => e.Country)
                .WithMany()
                .HasForeignKey(e => e.CountryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ApplicationUser>()
            .HasOne(e => e.Province)
            .WithMany()
            .HasForeignKey(e => e.ProvinceId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        // Seed data for the Province entity.
        modelBuilder.Entity<Province>().HasData(DataSeed.Provinces);
    }
}
