    using POS_API.Models;
    using POS_API.POS_Database;
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class UserRoleController : ApiController
    {
        private readonly POSEntities db;

        public UserRoleController()
        {
            this.db = new POSEntities();
        }

        public UserRoleController(POSEntities db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("api/userrole/create")]
        public HttpResponseMessage CreateUserRole([FromBody] RoleViewModel userRoleRequest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model");
            }

            try
            {
                UserRoleBusiness userRoleBusiness = new UserRoleBusiness(db);
                var response = userRoleBusiness.CreateUserRole(userRoleRequest);

                if (response.Status == OperationStatus.Success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "User role created successfully");
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
        }
        [HttpGet]
        [Route("api/role/rolelist")]
        public IHttpActionResult GetRoles()
        {
            try
            {
                var roles = db.Roles
                              .Select(r => new
                              {
                                  RoleId = r.RoleId,
                                  RoleName = r.RoleName,
                                  RoleDesc = r.RoleDesc,
                                  CreatedDate = r.CreatedDate
                              })
                              .ToList();

                return Ok(roles); // Return 200 OK with roles
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // Return 500 Internal Server Error with exception message
            }
        }

    }

