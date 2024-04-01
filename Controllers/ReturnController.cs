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
    public class ReturnController : ApiController
    {
        private readonly ReturnBusiness _returnBusiness;

        public ReturnController()
        {
            var db = new POSEntities(); // Create an instance of the database context
            var returnDAT = new ReturnDAT(db); // Create an instance of the data access object
            _returnBusiness = new ReturnBusiness(returnDAT); // Create an instance of the business logic class
        }

        [HttpPost]
        [Route("Api/Return/Add")]
        public HttpResponseMessage AddReturn([FromBody] ReturnViewModel returnRequest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Model");
            }

            try
            {
                var response = _returnBusiness.AddReturn(returnRequest);

                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Return added successfully");
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
        [Route("Api/Return/List")]
        public HttpResponseMessage GetReturnList()
        {
            try
            {
                var returnList = _returnBusiness.GetReturnList();

                return Request.CreateResponse(HttpStatusCode.OK, returnList);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred while fetching return list");
            }
        }

        [HttpPut]
        [Route("Api/Return/Update")]
        public HttpResponseMessage UpdateReturn([FromBody] ReturnViewModel returnRequest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Model");
            }

            try
            {
                var response = _returnBusiness.UpdateReturn(returnRequest);

                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Return updated successfully");
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
        [Route("Api/Return/Delete/{returnId}")]
        public HttpResponseMessage DeleteReturn(int returnId)
        {
            try
            {
                var response = _returnBusiness.DeleteReturn(returnId);

                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Return deleted successfully");
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
