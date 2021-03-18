using System.Collections.Generic;

namespace Places.Models
{
  public class City
  {
    public City()
    {
      this.Landmarks = new HashSet<Landmark>();
      this.JoinEntities = new HashSet<CityPerson>();
    }

    public int CityId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Landmark> Landmarks { get; set; }
    public virtual ICollection<CityPerson> JoinEntities { get; set; }
  }
}