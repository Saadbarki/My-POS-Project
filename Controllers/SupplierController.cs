using System.Web.Mvc;

namespace POS.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Supplierlist()
        {
            return View();
        }
        public ActionResult Addsupplier()
        {
            return View();
        }
    }
}