using Factory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace Factory.Controllers;

public class HomeController : Controller
{
  private readonly FactoryContext _db;

  public HomeController(FactoryContext db)
  {
    _db = db;
  }
  
  [HttpGet("/")]
  public ActionResult Index() 
  {
    if (TempData["Message"] != null)
    {
      ViewBag.Message = TempData["Message"];
      TempData.Remove("Message");
    }
    ViewBag.Engineers = _db.Engineers.ToList();
    ViewBag.Machines = _db.Machines.ToList();
    return View();
  }

}
