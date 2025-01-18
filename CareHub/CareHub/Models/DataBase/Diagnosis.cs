using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CareHub.Models.DataBase
{
    public class Diagnosis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiagnosisId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Height { get; set; } = "";
        public string Weight { get; set; } = "";
        public string BP { get; set; } = "";
        public string Fever { get; set; } = "";
        public string Complains { get; set; } = "";
        public string DiagnosisReason { get; set; } = "";
        public string Plans { get; set; } = "";
        public string AddedBy { get; set; } = "";
        public DateTime? AddedDate { get; set; }
        public string UpdatedBy { get; set; } = "";
        public DateTime? UpdatedDate { get; set; }
        public string InActiveBy { get; set; } = "";
        public bool? InActive { get; set; }

       
    }
}
