using Microsoft.IdentityModel.Tokens;
using POS_API.BusinessLogic;
using POS_API.DatabaseLogic;
using POS_API.Models;
using POS_API.POS_Database;
using System;
using System.Data.Entity.Validation;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace POS_API.Controllers
{
    public class UserController : ApiController
    {
        private readonly POSEntities db;  //Database context field

        public UserController()
        {
            this.db = new POSEntities();
        }

        public UserController(POSEntities db)
        {
            this.db = db; // injecting database context
        }

        [HttpPost]
        [Route("Api/User/signup")]
        public HttpResponseMessage Signup([FromBody] UserViewModel signupRequest)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Model");
            }

            try
            {
                // Validate that the password and confirm password match
                if (signupRequest.PasswordHash != signupRequest.ConfirmPassword)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Password and confirm password do not match");
                }

                // Hash the password before saving it to the database
                string hashedPassword = UsersBusiness.HashPassword(signupRequest.PasswordHash);

                // Save the hashed password to the database
                db.Users.Add(new User
                {
                    UserName = signupRequest.UserName,
                    Email = signupRequest.Email,
                    PasswordHash = hashedPassword,
                    ConfirmPassword = hashedPassword,
                    Address = signupRequest.Address,
                    City = signupRequest.City,
                    DateOfBirth = signupRequest.DateOfBirth,
                    FirstName = signupRequest.FirstName,
                    LastName = signupRequest.LastName,
                    PhoneNumber = signupRequest.PhoneNumber,
                    CreatedDate = signupRequest.CreatedDate ?? DateTime.Now
                    // Other user properties...
                });

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "User signed up successfully");
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                return Request.CreateResponse(HttpStatusCode.BadRequest, fullErrorMessage);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occurred: " + ex.Message);
            }
        }

       

        [HttpPost]
        [Route("Api/User/login")]
        public HttpResponseMessage Login([FromBody] UserViewModel loginRequest)
        {
            if (loginRequest == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid request body");
            }

            try
            {
                var usersBusiness = new UsersBusiness(db);
                var response = usersBusiness.UserLogin(loginRequest);

                if (response.Status == OperationStatus.Success)
                {
                    var token = GenerateJwtToken(response.UserID);
                    return Request.CreateResponse(HttpStatusCode.OK, new { Token = token });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid email or password");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occurred: " + ex.Message);
            }
        }
        [HttpPost]
        [Route("Api/User/UpdateUser")]
        public IHttpActionResult UpdateUser(UserViewModel user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User data is missing");
                }

                var usersBusiness = new UsersBusiness(db);
                var response = usersBusiness.UpdateUserDetails(user);

                if (response.Status == OperationStatus.Success)
                {
                    return Ok("User details updated successfully");
                }
                else
                {
                    return InternalServerError(new Exception(response.Message));
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred: " + ex.Message));
            }
        }

        //[EnableCors(origins: "https://localhost:44328", headers: "*", methods: "*")]
        //[HttpPost]
        //[Route("Api/User/forgotPassword")]
        //public IHttpActionResult ForgotPassword([FromBody] string email)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(email))
        //        {
        //            return BadRequest("Email address is required.");
        //        }

        //        var resetLink = "https://example.com/resetpassword"; // Placeholder reset link
        //        // Assuming you have a UsersBusiness class
        //        // var usersBusiness = new UsersBusiness(db);
        //        // var emailSent = usersBusiness.SendPasswordResetEmail(email, resetLink);

        //        if (emailSent)
        //        {
        //            return Ok("Password reset email sent successfully.");
        //        }
        //        else
        //        {
        //            return InternalServerError(new Exception("Failed to send password reset email."));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(new Exception("An error occurred: " + ex.Message));
        //    }
        //}

        [HttpGet]
        [Route("Api/User/List")]
        public IHttpActionResult GetUserList()
        {
            try
            {
                var usersBusiness = new UsersBusiness(db);
                var userList = usersBusiness.GetUserList();
                return Ok(userList);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred: " + ex.Message));
            }
        }

        [HttpPost]
        [Route("Api/User/Add")]
        public IHttpActionResult AddUser(UserViewModel user)
        {
            if (user == null)
            {
                return BadRequest("User data is missing");
            }

            // Check if the password is null or empty
            if (!User.IsInRole("admin"))
            {
                return BadRequest("Password cannot be null or empty");
            }

            user.PasswordHash = UsersBusiness.HashPassword(user.PasswordHash);

            try
            {
                var usersBusiness = new UsersBusiness(db); // Instantiate UsersBusiness
                usersBusiness.AddUser(user); // Call AddUser method
                var token = GenerateJwtToken((int)user.UserID); // Pass user.UserID directly
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred: " + ex.Message));
            }
        }

        private string GenerateJwtToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = GenerateRandomKey(256); // Generate a 256-bit key

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    // Add other claims as needed
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private byte[] GenerateRandomKey(int size)
        {
            if (size < 256)
            {
                throw new ArgumentException("Key size must be at least 256 bits.", nameof(size));
            }

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[size / 8]; // Convert bits to bytes
                rng.GetBytes(key);
                return key;
            }
        }



    }
}








