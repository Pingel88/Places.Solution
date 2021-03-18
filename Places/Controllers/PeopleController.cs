using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Places.Models;

namespace Places.Controllers
{
  public class PeopleController : Controller
  {
    private readonly PlacesContext _db;

    public PeopleController(PlacesContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.People.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.CityId = new SelectList(_db.Cities, "CityId", "Name");
      ViewBag.LandmarkId = new SelectList(_db.Landmarks, "LandmarkId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Person person, int CityId, int LandmarkId)
    {
      _db.People.Add(person);
      _db.SaveChanges();
      if (CityId != 0)
      {
        _db.CityPerson.Add(new CityPerson() { CityId = CityId, PersonId = person.PersonId });
      }
      if (LandmarkId != 0)
      {
        _db.LandmarkPerson.Add(new LandmarkPerson() { LandmarkId = LandmarkId, PersonId = person.PersonId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Person thisPerson = _db.People
        .Include(person => person.JoinCityPerson)
        .ThenInclude(join => join.City)
        .Include(person => person.JoinLandmarkPerson)
        .ThenInclude(join => join.Landmark)
        .FirstOrDefault(person => person.PersonId == id);
      return View(thisPerson);
    }

    public ActionResult Edit(int id)
    {
      Person thisPerson = _db.People.FirstOrDefault(person => person.PersonId == id);
      ViewBag.CityId = new SelectList(_db.Cities, "CityId", "Name");
      ViewBag.LandmarkId = new SelectList(_db.Landmarks, "LandmarkId", "Landmark");
      return View(thisPerson);
    }

    [HttpPost]
    public ActionResult Edit(Person person, int CityId, int LandmarkId)
    {
      if (CityId != 0)
      {
        _db.CityPerson.Add(new CityPerson() { CityId = CityId, PersonId = person.PersonId });
      }
      if (LandmarkId != 0)
      {
        _db.LandmarkPerson.Add(new LandmarkPerson() { LandmarkId = LandmarkId, PersonId = person.PersonId });
      }
      _db.Entry(person).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Person thisPerson = _db.People.FirstOrDefault(person => person.PersonId == id);
      _db.People.Remove(thisPerson);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Person thisPerson = _db.People.FirstOrDefault(person => person.PersonId == id);
      _db.People.Remove(thisPerson);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddCity (int id)
    {
      Person thisPerson = _db.People.FirstOrDefault(person => person.PersonId == id);
      ViewBag.CityId = new SelectList(_db.Cities, "CityId", "Name");
      return View(thisPerson);
    }

    [HttpPost]
    public ActionResult AddCity(Person person, int CityId)
    {
      if (CityId != 0)
      {
        _db.CityPerson.Add(new CityPerson() { CityId = CityId, PersonId = person.PersonId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

        public ActionResult AddLandmark (int id)
    {
      Person thisPerson = _db.People.FirstOrDefault(person => person.PersonId == id);
      ViewBag.LandmarkId = new SelectList(_db.Cities, "LandmarkId", "Name");
      return View(thisPerson);
    }

    [HttpPost]
    public ActionResult AddLandmark(Person person, int LandmarkId)
    {
      if (LandmarkId != 0)
      {
        _db.LandmarkPerson.Add(new LandmarkPerson() { LandmarkId = LandmarkId, PersonId = person.PersonId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteCity(int joinId)
    {
      CityPerson joinEntry = _db.CityPerson.FirstOrDefault(entry => entry.CityPersonId == joinId);
      _db.CityPerson.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteLandmark(int joinId)
    {
      LandmarkPerson joinEntry = _db.LandmarkPerson.FirstOrDefault(entry => entry.LandmarkPersonId == joinId);
      _db.LandmarkPerson.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}