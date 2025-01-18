using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareHub.Models.DataBase
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string AppointmentReason { get; set; } = "";
        public string Description { get; set; } = "";
        public string Address { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public DateTime? AppointmentDate { get; set; }
        public DateTime? Time { get; set; }
        public DateTime? CheckedInDateTime { get; set; }
        public DateTime? CheckedOutDateTime { get; set; }
        public bool? IsNotified { get; set; }
        public string AddedBy { get; set; } = "";
        public DateTime? AddedDate { get; set; }
        public string UpdatedBy { get; set; } = "";
        public DateTime? UpdatedDate { get; set; }
        public string InActiveBy { get; set; } = "";
        public bool? InActive { get; set; }

       
    }
}
