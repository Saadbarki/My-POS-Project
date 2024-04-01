using System;

namespace POS_API.Models
{
    public class RoleViewModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
