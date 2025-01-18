using CareHub.Models.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CareHub.Models.DbContext
{
    public class CareHubContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public CareHubContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}
