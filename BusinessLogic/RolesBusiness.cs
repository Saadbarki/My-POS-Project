using POS_API.Models;
using POS_API.POS_Database;
using System;

public class UserRoleBusiness
{
    private readonly POSEntities db;

    public UserRoleBusiness(POSEntities db)
    {
        this.db = db;
    }

    public ResponseViewModel CreateUserRole(RoleViewModel RoleRequest)
    {
        ResponseViewModel responseViewModel = new ResponseViewModel();
        try
        {
            UserRolesDAT userRolesDAT = new UserRolesDAT(db);
            responseViewModel = userRolesDAT.CreateUserRoles(RoleRequest);
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
