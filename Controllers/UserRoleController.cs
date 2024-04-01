    using POS_API.Models;
    using POS_API.POS_Database;
    using System.Net.Http;
    using System.Net;
    using System.Web.Http;
    using System.Linq;
    using System;

    public class RoleController : ApiController
    {
        private readonly POSEntities db;

        public RoleController()
        {
            this.db = new POSEntities();
        }

        public RoleController(POSEntities db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("api/role/create")]
        public HttpResponseMessage CreateRole([FromBody] RoleViewModel roleRequest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model");
            }

            try
            {
                RolesDAT rolesDAT = new RolesDAT(db);
                var response = rolesDAT.CreateRole(roleRequest);

                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Role created successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, response.Message);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occurred");
            }
        } // This is the end of the CreateRole method

        // The GetRoles method should be defined outside of the CreateRole method
        [HttpGet]
        [Route("api/role/list")]
        public IHttpActionResult GetRoles()
        {
            try
            {
                var roles = db.Roles.ToList(); // Retrieve all roles from the database
                return Ok(roles); // Return 200 OK with roles
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // Return 500 Internal Server Error with exception message
            }
        }
    }