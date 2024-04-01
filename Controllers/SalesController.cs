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
    public class SalesController : ApiController
    {
        private readonly SalesBusiness _salesBusiness;

        public SalesController()
        {
            var db = new POSEntities(); // Create an instance of the database context
            var salesDAT = new SalesDAT(db); // Create an instance of the data access object
            _salesBusiness = new SalesBusiness(salesDAT); // Create an instance of the business logic class
        }

        [HttpPost]
        [Route("Api/Sales/Add")]
        public HttpResponseMessage AddSales([FromBody] SalesViewModel salesRequest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Model");
            }

            try
            {
                // Call the method to add sales using the SalesBusiness instance
                var response = _salesBusiness.AddSales(salesRequest);

                // Check the response status and return an HTTP response
                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Sales added successfully");
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
        [Route("Api/Sales/List")]
        public HttpResponseMessage GetSalesList()
        {
            try
            {
                // Call the method to get the list of sales using the SalesBusiness instance
                var salesList = _salesBusiness.GetSalesList();

                // Return the list of sales in the HTTP response
                return Request.CreateResponse(HttpStatusCode.OK, salesList);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred while fetching sales list");
            }
        }

        [HttpGet]
        [Route("Api/Sales/Get/{id}")]
        public HttpResponseMessage GetSalesById(int id)
        {
            try
            {
                // Call the method to get sales by ID using the SalesBusiness instance
                var sales = _salesBusiness.GetSalesById(id);

                if (sales != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, sales);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Sales not found");
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred while fetching sales");
            }
        }

        [HttpPut]
        [Route("Api/Sales/Update/{id}")]
        public HttpResponseMessage UpdateSales(int id, [FromBody] SalesViewModel updatedSales)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Model");
            }

            try
            {
                // Call the method to update sales using the SalesBusiness instance
                var response = _salesBusiness.UpdateSales(id, updatedSales);

                // Check the response status and return an HTTP response
                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Sales updated successfully");
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
        [Route("Api/Sales/Delete/{id}")]
        public HttpResponseMessage DeleteSales(int id)
        {
            try
            {
                // Call the method to delete sales using the SalesBusiness instance
                var response = _salesBusiness.DeleteSales(id);

                // Check the response status and return an HTTP response
                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Sales deleted successfully");
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
