using Microsoft.EntityFrameworkCore;
using Pharmacy.src.models;

namespace Pharmacy.src.context
{
    public class PharmacyContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicationControl> MedicationControls { get; set; }

        public PharmacyContext(DbContextOptions<PharmacyContext> opt) : base(opt)
        {

        }
    }
}
