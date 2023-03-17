using Factory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace Factory.Controllers;

public class EngineersController : Controller
{
  private readonly FactoryContext _db;

  public EngineersController(FactoryContext db)
  {
    _db = db;
  }

  public ActionResult Index()
  {
    return View(_db.Engineers.Include(thing=>thing.RepairCerts).ThenInclude(thing=>thing.Machine).ToList());
  }

  public ActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public ActionResult Create(Engineer newEngineer, bool splashOrDetails)
  {
    _db.Engineers.Add(newEngineer);
    _db.SaveChanges();
    if(splashOrDetails)
    {return RedirectToAction("Details", new {id = newEngineer.EngineerId});}
    else
    {return RedirectToAction("Index");}
  }

  public ActionResult Details(int id)
  {

    Engineer model = _db.Engineers
      .Include(thing=>thing.RepairCerts)
      .ThenInclude(thing=>thing.Machine)
      .FirstOrDefault(otherthing => (otherthing.EngineerId == id));
      
    return View(model);
  }
}
