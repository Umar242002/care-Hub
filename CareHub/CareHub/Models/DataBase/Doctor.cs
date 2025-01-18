using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CareHub.Models.DataBase
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string DoctorName { get; set; } = "";
        public string Mobile { get; set; } = "";
        public string BusinessMobile { get; set; } = "";
        public string Experience { get; set; } = "";
        public string Country { get; set; } = "";
        public string State { get; set; } = "";
        public string City { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public string DOB { get; set; } = "";
        public string Address { get; set; } = "";
        public string Degree { get; set; } = "";
        public string CNIC { get; set; } = "";
        public string AddedBy { get; set; } = "";
        public DateTime? AddedDate { get; set; }
        public string UpdatedBy { get; set; } = "";
        public DateTime? UpdatedDate { get; set; }
        public string InActiveBy { get; set; } = "";
        public bool? InActive { get; set; }
    }
}
