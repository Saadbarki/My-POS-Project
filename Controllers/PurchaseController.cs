using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using POS_API.BusinessLogic;
using POS_API.DatabaseLogic;
using POS_API.Models;
using POS_API.POS_Database;

namespace POS_API.Controllers
{
    public class PurchaseController : ApiController
    {
        private readonly PurchaseBusiness _purchaseBusiness;

        public PurchaseController()
        {
            var db = new POSEntities();
            _purchaseBusiness = new PurchaseBusiness(db);
        }

        [HttpPost]
        [Route("Api/Purchase/Add")]
        public HttpResponseMessage AddPurchase([FromBody] PurchaseViewModel purchaseRequest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Model");
            }

            try
            {
                _purchaseBusiness.AddPurchase(purchaseRequest);
                return Request.CreateResponse(HttpStatusCode.OK, "Purchase added successfully");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred");
            }
        }

        [HttpGet]
        [Route("Api/Purchase/GetAll")]
        public HttpResponseMessage GetAllPurchases()
        {
            try
            {
                var purchases = _purchaseBusiness.GetAllPurchases();
                return Request.CreateResponse(HttpStatusCode.OK, purchases);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Failed to retrieve purchases");
            }
        }

        // Add other CRUD actions as needed
    }
}
