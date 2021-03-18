namespace Places.Models
{
  public class CityPerson
  {
    public int CityPersonId { get; set; }
    public int CityId { get; set; }
    public int PersonId { get; set; }
    public virtual City City { get; set; }
    public virtual Person Person { get; set; }
  }
}