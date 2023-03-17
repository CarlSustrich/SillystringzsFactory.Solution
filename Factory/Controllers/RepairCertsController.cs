using Factory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace Factory.Controllers;

public class RepairCertsController : Controller
{
  private readonly FactoryContext _db;

  public RepairCertsController(FactoryContext db)
  {
    _db = db;
  }

  public ActionResult Delete(List<int> deleteWhich)
  {

    foreach(int item in deleteWhich)
    {
      _db.RepairCerts.Remove(_db.RepairCerts.FirstOrDefault(thing=>thing.RepairCertId == item));
      _db.SaveChanges();
    }
    TempData["Message"] = "Removed the selected Repair Certifications";
    return RedirectToAction("Index", "Home");
  }
}
