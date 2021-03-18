using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Places.Models;

namespace Places.Controllers
{
  public class LandmarksController : Controller
  {
    private readonly PlacesContext _db;

    public LandmarksController(PlacesContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Landmark> model = _db.Landmarks.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.CityId = new SelectList(_db.Cities, "CityId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Landmark landmark)
    {
      _db.Landmarks.Add(landmark);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Landmark thisLandmark = _db.Landmarks
        .Include(landmark => landmark.JoinEntities)
        .ThenInclude(join => join.Person)
        .FirstOrDefault(landmark => landmark.LandmarkId == id);
      return View(thisLandmark);
    }

    public ActionResult Edit(int id)
    {
      Landmark thisLandmark = _db.Landmarks.FirstOrDefault(landmark => landmark.LandmarkId == id);
      return View(thisLandmark);
    }

    [HttpPost]
    public ActionResult Edit (Landmark landmark)
    {
      _db.Entry(landmark).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Landmark thisLandmark = _db.Landmarks.FirstOrDefault(landmark => landmark.LandmarkId == id);
      return View(thisLandmark);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Landmark thisLandmark = _db.Landmarks.FirstOrDefault(landmark => landmark.LandmarkId == id);
      _db.Landmarks.Remove(thisLandmark);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}