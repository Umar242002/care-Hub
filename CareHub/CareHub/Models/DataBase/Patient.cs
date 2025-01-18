using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareHub.Models.DataBase
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string PatientName { get; set; } = "";
        public string CNIC { get; set; } = "";
        public string DOB { get; set; } = "";
        public string Gender { get; set; } = "";
        public string Email { get; set; } = "";
        public string PatientAddress { get; set; } = "";
        public int? DoctorId { get; set; }
        public string AddedBy { get; set; } = "";
        public DateTime? AddedDate { get; set; }
        public string UpdatedBy { get; set; } = "";
        public DateTime? UpdatedDate { get; set; }
        public string InActiveBy { get; set; } = "";
        public bool? InActive { get; set; }
    }
}
