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
    if (TempData["Message"] != null)
    {
      ViewBag.Message = TempData["Message"];
      TempData.Remove("Message");
    }
    return View(_db.Engineers.Include(thing=>thing.RepairCerts).ThenInclude(thing=>thing.Machine).ToList());
  }

  public ActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public ActionResult Create(Engineer newEngineer, bool splashOrDetails)
  {
    if(!ModelState.IsValid) {return View();}
    _db.Engineers.Add(newEngineer);
    _db.SaveChanges();
    if(splashOrDetails)
    {return RedirectToAction("AddCert", new {id = newEngineer.EngineerId});}
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

  public ActionResult AddCert(int id)
  {
    ViewBag.Machines = _db.Machines.ToList();
    return View(_db.Engineers.FirstOrDefault(peep=>peep.EngineerId == id));
  }

  [HttpPost]
  public ActionResult AddCert(List<int> wutMachines, int engineerId)
  {
    if(wutMachines.Count == 0)
    {
      @ViewBag.Success = "No machines were selected";
      ViewBag.Machines = _db.Machines.ToList();
      return View(_db.Engineers.FirstOrDefault(peep=>peep.EngineerId == engineerId));
    }
    
    foreach (int item in wutMachines)
    {
      #nullable enable
      RepairCert? joinCheck = _db.RepairCerts.FirstOrDefault(genericPlaceHolderVariableName => (genericPlaceHolderVariableName.EngineerId == engineerId && genericPlaceHolderVariableName.MachineId == item));
      #nullable disable
      if(joinCheck == null && engineerId != 0)
      {
        _db.RepairCerts.Add(new RepairCert() {MachineId=item, EngineerId=engineerId});
        _db.SaveChanges();
      }
    }
    return RedirectToAction("Details", new {id = engineerId});
  }

  [HttpPost]
  public ActionResult MassDelete(List<int> deleteWho)
  {
    foreach(int item in deleteWho)
    {
      _db.Engineers.Remove(_db.Engineers.FirstOrDefault(person=>person.EngineerId == item));
      _db.SaveChanges();
    }
    TempData["Message"] = "Deleted the selected Engineers";
    return RedirectToAction("Index");
  }

  public ActionResult Edit(int id)
  {
    return View(_db.Engineers.FirstOrDefault(thing=>thing.EngineerId == id));
  }

  [HttpPost]
  public ActionResult Edit(Engineer updatedEngineer)
  {
    _db.Engineers.Update(updatedEngineer);
    _db.SaveChanges();
    TempData["Message"] = "Engineer's Info Updated";
    return RedirectToAction("Index");
  }
}
