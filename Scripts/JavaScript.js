//function navigateToSignUp() {
//    // Redirect to the signup page
//    try {
//        window.location.href = "/User/Signup";

//    } catch (e) {

//    }
//}


//// Function to toggle user list display
//function toggleUserList() {
//    const userList = document.getElementById("user-list");
//    userList.style.display = "block"; // Show user list
//    // Hide other submenus if needed
//}
//// JavaScript.js

//// Function to toggle user list
//function toggleUserList() {
//    var userList = document.getElementById("user-list");
//    var addUserForm = document.getElementById("add-user-form");
//    var roleList = document.getElementById("role-list");
//    var addRoleForm = document.getElementById("add-role-form");

//    userList.style.display = "block";
//    addUserForm.style.display = "none";
//    roleList.style.display = "none";
//    addRoleForm.style.display = "none";
//}
//function deleteRole(roleId) {
//    // Confirm deletion
//    if (confirm('Are you sure you want to delete this role?')) {
//        // Find the row corresponding to the role ID and remove it
//        var row = document.querySelector('tr[data-role-id="' + roleId + '"]');
//        if (row) {
//            row.remove();
//        }
//    }
//}

//function deleteUser(userId) {
//    // Confirm deletion
//    if (confirm('Are you sure you want to delete this user?')) {
//        // Find the row corresponding to the user ID and remove it
//        var row = document.querySelector('tr[data-user-id="' + userId + '"]');
//        if (row) {
//            row.remove();
//        }
//    }
//}
// Function to navigate to the signup page
// Function to navigate to the signup page
// Function to navigate to the signup page
function navigateToSignUp() {
    try {
        window.location.href = "/User/Signup";
    } catch (e) {
        console.error(e);
    }
}

// Function to toggle user list display
function toggleUserList() {
    const userList = document.getElementById("user-list");
    userList.style.display = "block"; // Show user list
    // Hide other submenus if needed
}

// Function to delete role
function deleteRole(roleId) {
    // Confirm deletion
    if (confirm('Are you sure you want to delete this role?')) {
        // Find the row corresponding to the role ID and remove it
        var row = document.querySelector('tr[data-role-id="' + roleId + '"]');
        if (row) {
            row.remove();
        }
    }
}

// Function to delete user
function deleteUser(userId) {
    // Confirm deletion
    if (confirm('Are you sure you want to delete this user?')) {
        // Assuming you have jQuery included in your project
        $.ajax({
            url: '/User/DeleteUser',
            type: 'POST',
            data: { userId: userId },
            success: function (result) {
                // Handle success
                // For example, show success message or update user list
                location.reload();
            },
            error: function (xhr, status, error) {
                // Handle error
                console.error(xhr.responseText);
            }
        });
    }
}

// JavaScript function to open edit user modal and populate data
function openEditUserModal(userId) {
    // Assuming you have jQuery included in your project
    $.ajax({
        url: '/User/GetUserById',
        type: 'GET',
        data: { userId: userId },
        success: function (data) {
            // Populate the modal fields with user data
            $('#editUserName').val(data.UserName);
            // You can populate other fields similarly
            // Show the modal
            $('#editUserModal').modal('show');
        },
        error: function (xhr, status, error) {
            // Handle error
            console.error(xhr.responseText);
        }
    });
}

// JavaScript function to save user changes
function saveUserChanges() {
    // Assuming you have jQuery included in your project
    $.ajax({
        url: '/User/SaveUserChanges', // Replace with your action URL
        type: 'POST',
        data: $('#editUserForm').serialize(), // Serialize form data
        success: function (result) {
            // Handle success
            // For example, close modal or show success message
            $('#editUserModal').modal('hide');
            // Reload the page or update user list as needed
            location.reload();
        },
        error: function (xhr, status, error) {
            // Handle error
            console.error(xhr.responseText);
        }
    });
}



