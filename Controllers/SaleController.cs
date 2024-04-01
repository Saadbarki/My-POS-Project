using System.Web.Mvc;

namespace POS.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        public ActionResult addsale()
        {
            return View();
        }
        public ActionResult salelist()
        {
            return View();
        }
    }
}