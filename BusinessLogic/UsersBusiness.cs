
using POS_API.DatabaseLogic;
using POS_API.Models;
using POS_API.POS_Database;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace POS_API.BusinessLogic
{
    public class UsersBusiness
    {
        private readonly POSEntities db;

        public UsersBusiness(POSEntities db)
        {
            this.db = db;
        }

        public ResponseViewModel UserRegistration(UserViewModel userMode)
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();

            try
            {
                UsersDAT userDBLogic = new UsersDAT(db);
                userMode.PasswordHash = HashPassword(userMode.PasswordHash);
                responseViewModel = userDBLogic.UserRegistration(userMode);
            }
            catch (Exception ex)
            {
                HandleException(responseViewModel, ex);
            }

            return responseViewModel;
        }

        public ResponseViewModel UserLogin(UserViewModel loginModel)
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();

            try
            {
                UsersDAT userDBLogic = new UsersDAT(db);
                responseViewModel = userDBLogic.UserLogin(loginModel);
            }
            catch (Exception ex)
            {
                HandleException(responseViewModel, ex);
            }

            return responseViewModel;
        }


        public ResponseViewModel UpdateUserDetails(UserViewModel userUVM)
        {
            ResponseViewModel response = new ResponseViewModel();

            try
            {
                if (userUVM == null)
                {
                    response.Status = OperationStatus.Failure;
                    response.Description = OperationDescription.Error;
                    response.Message = "User data is missing";
                    return response;
                }

                var userDat = new UsersDAT(db);
                var user = userDat.UpdateUser(userUVM);
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return response;
        }
        public static byte[] GenerateRandomKey(int size)
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


        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty");
            }

            // Generate a random salt
            byte[] salt = GenerateRandomKey(256); // 256-bit salt (adjust size as needed)

            // Hash password with salt
            using (var sha256Hash = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                byte[] hashedPassword = sha256Hash.ComputeHash(saltedPassword);

                // Concatenate salt and hashed password
                byte[] combinedBytes = new byte[salt.Length + hashedPassword.Length];
                Buffer.BlockCopy(salt, 0, combinedBytes, 0, salt.Length);
                Buffer.BlockCopy(hashedPassword, 0, combinedBytes, salt.Length, hashedPassword.Length);

                // Convert to Base64 string for storage
                return Convert.ToBase64String(combinedBytes);
            }
        }
        public void AddUser(UserViewModel userViewModel)
        {
            // Map UserViewModel to User entity or any necessary data manipulation
            User user = new User
            {
                UserName = userViewModel.UserName,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Email = userViewModel.Email,
                PasswordHash = UsersBusiness.HashPassword(userViewModel.PasswordHash), // Use the existing HashPassword method
                PhoneNumber = userViewModel.PhoneNumber,
                Address = userViewModel.Address,
                City = userViewModel.City,
                DateOfBirth = userViewModel.DateOfBirth,
                CreatedDate = userViewModel.CreatedDate,
                ResetToken = userViewModel.ResetToken,
                Gender = userViewModel.Gender,
                ConfirmPassword = userViewModel.ConfirmPassword,
                Status = userViewModel.Status,
                NotifyByEmail = userViewModel.NotifyByEmail
            };

            // Add user to database context and save changes
            db.Users.Add(user);
            db.SaveChanges(); // Save changes to the database
        }



        public List<UserViewModel> GetUserList()
        {
            try
            {
                UsersDAT userDat = new UsersDAT(db);
                return userDat.GetUserList();
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine("Error in GetUserList: " + ex.Message);
                return new List<UserViewModel>(); // Return an empty list or handle the exception according to your application's logic
            }
        }

        private void HandleException(ResponseViewModel response, Exception ex)
        {
            response.Status = OperationStatus.Failure;
            response.Description = OperationDescription.Error;
            response.Message = ex.Message;
        }
    }
}




