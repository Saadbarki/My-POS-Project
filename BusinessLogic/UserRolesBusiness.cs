using POS_API.DatabaseLogic;
using POS_API.Models;
using POS_API.POS_Database;
using System;

namespace POS_API.BusinessLogic
{
    public class RoleBusiness
    {
        private readonly POSEntities db; // Database context field

        public RoleBusiness(POSEntities db)
        {
            this.db = db; // Initialize database context
        }

        public ResponseViewModel CreateUserRole(RoleViewModel roleRequest)
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();
            try
            {
                // Create an instance of user role data access logic class
                RolesDAT RoleDAT = new RolesDAT(db);
                // Call the method to create a user role
                responseViewModel = RoleDAT.CreateRole(roleRequest);
            }
            catch (Exception ex)
            {
                responseViewModel.Status = OperationStatus.Failure;
                responseViewModel.Description = OperationDescription.Error;
                responseViewModel.Message = ex.Message;
            }
            return responseViewModel;
        }

        // Add other methods for managing user roles as needed
    }
}
