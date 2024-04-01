using System.Web.Mvc;

namespace POS.Controllers
{
    public class ReturnController : Controller
    {
        // GET: Return
        public ActionResult Returnlist()
        {
            return View();
        }
        public ActionResult Addreturn()
        {
            return View();
        }
    }
}