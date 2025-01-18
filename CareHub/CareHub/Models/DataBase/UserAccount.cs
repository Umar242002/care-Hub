using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CareHub.Models.DataBase
{
    public class UserAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string UserRole { get; set; } = "";
        public string Role { get; set; } = "";
        public string UserType { get; set; } = "";
        public bool? IsApproved { get; set; } 
        public string ApprovedBy { get; set; } = "";
        public bool? IsUserBlock { get; set; }
        public string BlockNote { get; set; } = "";
        public string AddedBy { get; set; } = "";
        public DateTime? AddedDate { get; set; }
        public string UpdatedBy { get; set; } = "";
        public DateTime? UpdatedDate { get; set; } 
        public string InActiveBy { get; set; } = "";
        public bool? InActive { get; set; }
    }
}
