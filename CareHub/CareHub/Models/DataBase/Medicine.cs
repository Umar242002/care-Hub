using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CareHub.Models.DataBase
{
    public class Medicine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MedicineId { get; set; }
        public string MedicineName { get; set; } = "";
        public int? PatientId { get; set; }
        public string AddedBy { get; set; } = "";
        public DateTime? AddedDate { get; set; }
        public string UpdatedBy { get; set; } = "";
        public DateTime? UpdatedDate { get; set; }
        public string InActiveBy { get; set; } = "";
        public bool? InActive { get; set; }

    }
}
