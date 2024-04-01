// SupplierController.cs
using POS_API.BusinessLogic;
using POS_API.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POS_API.Controllers
{
    public class SupplierController : ApiController
    {
        private readonly SupplierBusiness _supplierBusiness;

        public SupplierController()
        {
            _supplierBusiness = new SupplierBusiness();
        }

        [HttpPost]
        [Route("Api/Supplier/Add")]
        public HttpResponseMessage AddSupplier([FromBody] SupplierViewModel supplierRequest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Model");
            }

            try
            {
                var response = _supplierBusiness.AddSupplier(supplierRequest);

                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Supplier added successfully");
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
        [Route("Api/Supplier/List")]
        public HttpResponseMessage GetSupplierList()
        {
            try
            {
                var supplierList = _supplierBusiness.GetSupplierList();

                return Request.CreateResponse(HttpStatusCode.OK, supplierList);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred while fetching supplier list");
            }
        }

        [HttpPut]
        [Route("Api/Supplier/Update")]
        public HttpResponseMessage UpdateSupplier([FromBody] SupplierViewModel supplierRequest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Model");
            }

            try
            {
                var response = _supplierBusiness.UpdateSupplier(supplierRequest);

                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Supplier updated successfully");
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
        [Route("Api/Supplier/Delete/{id}")]
        public HttpResponseMessage DeleteSupplier(int id)
        {
            try
            {
                var response = _supplierBusiness.DeleteSupplier(id);

                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Supplier deleted successfully");
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
