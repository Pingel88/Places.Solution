using System.Collections.Generic;

namespace Places.Models
{
  public class Landmark
    {
      public Landmark()
      {
        this.JoinEntities = new HashSet<LandmarkPerson>();
      }

      public int LandmarkId { get; set; }
      public string Name { get; set; }
      public int CityId { get; set; }
      public virtual City city { get; set; }
      public virtual ICollection<LandmarkPerson> JoinEntities { get; set; }
  }
}