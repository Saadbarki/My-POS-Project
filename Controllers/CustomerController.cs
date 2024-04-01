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
    public class CustomerController : ApiController
    {
        private readonly CustomerBusiness _customerBusiness;

        public CustomerController()
        {
            var db = new POSEntities(); // Create an instance of the database context
            var customerDAT = new CustomerDAT(db); // Create an instance of the data access object
            _customerBusiness = new CustomerBusiness(customerDAT); // Create an instance of the business logic class
        }

        [HttpPost]
        [Route("Api/Customer/Add")]
        public HttpResponseMessage AddCustomer([FromBody] CustomerViewModel customerRequest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Model");
            }

            try
            {
                // Call the method to add customer using the CustomerBusiness instance
                var response = _customerBusiness.AddCustomer(customerRequest);

                // Check the response status and return an HTTP response
                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Customer added successfully");
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
        [Route("Api/Customer/List")]
        public HttpResponseMessage GetCustomerList()
        {
            try
            {
                // Call the method to get the list of customers using the CustomerBusiness instance
                var customerList = _customerBusiness.GetCustomerList();

                // Return the list of customers in the HTTP response
                return Request.CreateResponse(HttpStatusCode.OK, customerList);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred while fetching customer list");
            }
        }

        [HttpPut]
        [Route("Api/Customer/Update")]
        public HttpResponseMessage UpdateCustomer([FromBody] CustomerViewModel customerRequest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Model");
            }

            try
            {
                // Call the method to update customer using the CustomerBusiness instance
                var response = _customerBusiness.UpdateCustomer(customerRequest);

                // Check the response status and return an HTTP response
                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Customer updated successfully");
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
        [Route("Api/Customer/Delete/{id}")]
        public HttpResponseMessage DeleteCustomer(int id)
        {
            try
            {
                // Call the method to delete customer using the CustomerBusiness instance
                var response = _customerBusiness.DeleteCustomer(id);

                // Check the response status and return an HTTP response
                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Customer deleted successfully");
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
