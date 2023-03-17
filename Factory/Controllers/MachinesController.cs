using Factory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace Factory.Controllers;

public class MachinesController : Controller
{
  private readonly FactoryContext _db;

  public MachinesController(FactoryContext db)
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
    return View(_db.Machines.Include(thing=>thing.RepairCerts).ThenInclude(thing=>thing.Engineer).ToList());
  }

  public ActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public ActionResult Create(Machine newMachine, bool splashOrDetails)
  {
    _db.Machines.Add(newMachine);
    _db.SaveChanges();
    if(splashOrDetails)
    {return RedirectToAction("AddCert", new {id = newMachine.MachineId});}
    else
    {return RedirectToAction("Index");}
  }

  public ActionResult Details(int id)
  {
    Machine model = _db.Machines
      .Include(thing=>thing.RepairCerts)
      .ThenInclude(thing=>thing.Engineer)
      .FirstOrDefault(otherthing => (otherthing.MachineId == id));
    return View(model);
  }

  public ActionResult AddCert(int id)
  {
    ViewBag.Engineers = _db.Engineers.ToList();
    return View(_db.Machines.FirstOrDefault(peep=>peep.MachineId == id));
  }

  [HttpPost]
  public ActionResult AddCert(List<int> wutEngineers, int machineId)
  {
    if(wutEngineers.Count == 0)
    {
      @ViewBag.Success = "No engineers were selected";
      ViewBag.Engineers = _db.Engineers.ToList();
      return View(_db.Machines.FirstOrDefault(peep=>peep.MachineId == machineId));
    }
    
    foreach (int item in wutEngineers)
    {
      #nullable enable
      RepairCert? joinCheck = _db.RepairCerts.FirstOrDefault(genericPlaceHolderVariableName => (genericPlaceHolderVariableName.MachineId == machineId && genericPlaceHolderVariableName.EngineerId == item));
      #nullable disable
      if(joinCheck == null && machineId != 0)
      {
        _db.RepairCerts.Add(new RepairCert() {MachineId=machineId, EngineerId=item});
        _db.SaveChanges();
      }
    }
    return RedirectToAction("Details", new {id = machineId});
  }

  [HttpPost]
  public ActionResult MassDelete(List<int> deleteWut)
  {
    foreach(int item in deleteWut)
    {
      _db.Machines.Remove(_db.Machines.FirstOrDefault(person=>person.MachineId == item));
      _db.SaveChanges();
    }
    TempData["Message"] = "Deleted the selected Machines";
    return RedirectToAction("Index");
  }

  public ActionResult Edit(int id)
  {
    return View(_db.Machines.FirstOrDefault(thing=>thing.MachineId == id));
  }

  [HttpPost]
  public ActionResult Edit(Machine updatedMachine)
  {
    _db.Machines.Update(updatedMachine);
    _db.SaveChanges();
    TempData["Message"] = "Machine's Info Updated";
    return RedirectToAction("Index");
  }
}
