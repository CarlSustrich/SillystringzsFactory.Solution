using Microsoft.AspNetCore.Mvc;
namespace Factory.Controllers;

public class HomeController : Controller
{

  [Route("/")]
  public ActionResult Index() 
  {
    return View();
  }

}
