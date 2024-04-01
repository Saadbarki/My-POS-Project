using System.Web.Mvc;

namespace POS.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult categoriesadd()
        {
            return View();
        }
        public ActionResult categorieslist()
        {
            return View();
        }
    }
}