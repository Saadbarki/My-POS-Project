using System.Web.Mvc;

namespace POS.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult customerlist()
        {
            return View();
        }
        public ActionResult addcustomer()
        {
            return View();
        }
    }
}