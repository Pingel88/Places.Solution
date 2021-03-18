using Microsoft.EntityFrameworkCore;

namespace Places.Models
{
  public class PlacesContext : DbContext
  {
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Landmark> Landmarks { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<CityPerson> CityPerson { get; set; }
    public DbSet<LandmarkPerson> LandmarkPerson { get; set; }

    public PlacesContext(DbContextOptions options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}