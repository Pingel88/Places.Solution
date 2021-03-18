using System.Collections.Generic;

namespace Places.Models
{
  public class Person
  {
    public Person()
    {
      this.JoinCityPerson = new HashSet<CityPerson>();
      this.JoinLandmarkPerson = new HashSet<LandmarkPerson>();
    }

    public int PersonId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<CityPerson> JoinCityPerson { get;}
    public virtual ICollection<LandmarkPerson> JoinLandmarkPerson { get;}
  }
}