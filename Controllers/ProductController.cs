using System.Web.Mvc;

namespace POS.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Indexlist()
        {
            return View();
        }

        // GET: Product
        public ActionResult IndexAdd()
        {
            return View();
        }
    }
}