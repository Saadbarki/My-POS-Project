using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using POS_API.DatabaseLogic;
using POS_API.POS_Database;
using System.Web.Http.Cors;

namespace POS_API.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly POSEntities _db; // Database context field
        private readonly CategoriesDAT _categoriesDAT; // Category data access object

        public CategoriesController()
        {
            _db = new POSEntities();
            _categoriesDAT = new CategoriesDAT(_db);
        }

        public CategoriesController(POSEntities db, CategoriesDAT categoriesDAT)
        {
            _db = db; // injecting database context
            _categoriesDAT = categoriesDAT; // injecting category data access object
        }

        [HttpPost]
        [Route("Api/Category/Add")]
        public HttpResponseMessage AddCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Model");
            }

            try
            {
                _categoriesDAT.AddCategory(category);
                return Request.CreateResponse(HttpStatusCode.OK, "Category added successfully");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred");
            }
        }

        [HttpGet]
        [Route("Api/Category/GetAll")]
        public HttpResponseMessage GetAllCategories()
        {
            try
            {
                var categories = _categoriesDAT.GetAllCategories();
                return Request.CreateResponse(HttpStatusCode.OK, categories);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Failed to retrieve categories");
            }
        }

        [HttpGet]
        [Route("Api/Category/Get/{id}")]
        public HttpResponseMessage GetCategory(int id)
        {
            try
            {
                var category = _categoriesDAT.GetCategory(id);
                if (category == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Category not found");

                return Request.CreateResponse(HttpStatusCode.OK, category);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Failed to retrieve category");
            }
        }

        [HttpPut]
        [Route("Api/Category/Update/{id}")]
        public HttpResponseMessage UpdateCategory(int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Model");
            }

            try
            {
                var existingCategory = _categoriesDAT.GetCategory(id);
                if (existingCategory == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Category not found");

                category.category_ID = id; // Set the ID of the category to be updated
                _categoriesDAT.UpdateCategory(category);
                return Request.CreateResponse(HttpStatusCode.OK, "Category updated successfully");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred");
            }
        }

        [HttpDelete]
        [Route("Api/Category/Delete/{id}")]
        public HttpResponseMessage DeleteCategory(int id)
        {
            try
            {
                var existingCategory = _categoriesDAT.GetCategory(id);
                if (existingCategory == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Category not found");

                _categoriesDAT.DeleteCategory(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Category deleted successfully");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred");
            }
        }
    }
}
