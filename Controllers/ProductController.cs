using POS_API.BusinessLogic;
using POS_API.DatabaseLogic;
using POS_API.Models;
using POS_API.POS_Database;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POS_API.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ProductBusiness _productBusiness;

        public ProductController()
        {
            var db = new POSEntities(); // Create an instance of the database context
            var productDAT = new ProductDAT(db); // Create an instance of the data access object
            _productBusiness = new ProductBusiness(productDAT); // Create an instance of the business logic class
        }

        [HttpPost]
        [Route("Api/Product/Add")]
        public HttpResponseMessage AddProduct([FromBody] ProductViewModel productRequest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Model");
            }

            try
            {
                // Call the method to add product using the ProductBusiness instance
                var response = _productBusiness.AddProduct(productRequest);

                // Check the response status and return an HTTP response
                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Product added successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, response.Message);
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred");
            }
        }

        [HttpGet]
        [Route("Api/Product/List")]
        public HttpResponseMessage GetProductList()
        {
            try
            {
                // Call the method to get the list of products using the ProductBusiness instance
                var productList = _productBusiness.GetProductList();

                // Return the list of products in the HTTP response
                return Request.CreateResponse(HttpStatusCode.OK, productList);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred while fetching product list");
            }
        }

        [HttpPut]
        [Route("Api/Product/Update")]
        public HttpResponseMessage UpdateProduct([FromBody] ProductViewModel productRequest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Model");
            }

            try
            {
                // Call the method to update product using the ProductBusiness instance
                var response = _productBusiness.UpdateProduct(productRequest);

                // Check the response status and return an HTTP response
                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Product updated successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, response.Message);
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred");
            }
        }

        [HttpDelete]
        [Route("Api/Product/Delete/{id}")]
        public HttpResponseMessage DeleteProduct(int id)
        {
            try
            {
                // Call the method to delete product using the ProductBusiness instance
                var response = _productBusiness.DeleteProduct(id);

                // Check the response status and return an HTTP response
                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Product deleted successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, response.Message);
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred");
            }
        }
    }
}
