namespace Places.Models
{
  public class LandmarkPerson
  {
    public int LandmarkPersonId { get; set; }
    public int CityId { get; set; }
    public int PersonId { get; set; }
    
    public virtual Landmark landmark { get; set; }
    public virtual Person person { get; set; }
  }
}