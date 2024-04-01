using System.Web.Mvc;

namespace POS.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult Addpurchase()
        {
            return View();
        }
        public ActionResult Purchaselist()
        {
            return View();
        }
    }
}