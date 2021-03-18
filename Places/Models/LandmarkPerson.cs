namespace Places.Models
{
  public class LandmarkPerson
  {
    public int LandmarkPersonId { get; set; }
    public int LandmarkId { get; set; }
    public int PersonId { get; set; }
    
    public virtual Landmark Landmark { get; set; }
    public virtual Person Person { get; set; }
  }
}