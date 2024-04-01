using System;
using System.ComponentModel.DataAnnotations;

public class UserViewModel
{
    public int? UserID { get; set; }
    public string UserName { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(255)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(255)]
    public string LastName { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address")]
    [StringLength(255)]
    public string Email { get; set; }

    [StringLength(64)]
    public string PasswordHash { get; set; }

    [StringLength(20)]
    public string PhoneNumber { get; set; }

    [StringLength(255)]
    public string Address { get; set; }

    [StringLength(100)]
    public string City { get; set; }

    public DateTime? DateOfBirth { get; set; }
    public DateTime? CreatedDate { get; set; } = DateTime.Now;

    [StringLength(255)]
    public string ResetToken { get; set; }

    [StringLength(50)]
    public string Gender { get; set; }

    [StringLength(64)]
    public string ConfirmPassword { get; set; }

    [StringLength(100)]
    public string Status { get; set; }

    public bool? NotifyByEmail { get; set; }
}
