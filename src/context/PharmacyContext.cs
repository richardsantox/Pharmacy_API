using Microsoft.EntityFrameworkCore;
using Pharmacy.src.models;

namespace Pharmacy.src.context
{
    /// <summary>
    /// <para>Resumo: Classe contexto, responsável por carregar contexto e definir DbSets</para>
    /// <para>Criado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 09/07/2022</para>
    /// </summary>
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
