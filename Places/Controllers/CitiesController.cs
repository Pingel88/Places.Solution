using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Places.Models;

namespace Places.Controllers
{
  public class CitiesController : Controller
  {
    private readonly PlacesContext _db;

    public CitiesController(PlacesContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<City> model = _db.Cities.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(City city)
    {
      _db.Cities.Add(city);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      City thisCity = _db.Cities
        .Include(city => city.JoinEntities)
        .ThenInclude(join => join.Person)
        .FirstOrDefault(city => city.CityId == id);
      return View(thisCity);
    }
    
    public ActionResult Edit(int id)
    {
      City thisCity = _db.Cities.FirstOrDefault(city => city.CityId == id);
      return View(thisCity);
    }

    [HttpPost]
    public ActionResult Edit(City city)
    {
      _db.Entry(city).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      City thisCity = _db.Cities.FirstOrDefault(city => city.CityId == id);
      return View(thisCity);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      City thisCity = _db.Cities.FirstOrDefault(city => city.CityId == id);
      _db.Cities.Remove(thisCity);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}