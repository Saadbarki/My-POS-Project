using System.Web.Mvc;

namespace POS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        // Move the Welcome action method outside of the Contact method
        public ActionResult Welcome()
        {
            // You can pass additional data to the view if needed
            ViewBag.Message = "Welcome! Your registration was successful.";
            return View();
        }
    }
}
