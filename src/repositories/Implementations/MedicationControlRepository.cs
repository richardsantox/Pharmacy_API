using Microsoft.EntityFrameworkCore;
using Pharmacy.src.context;
using Pharmacy.src.models;
using System.Threading.Tasks;

namespace Pharmacy.src.repositories.Implementations
{
    public class MedicationControlRepository : IMedicationControl
    {
        #region Attributes

        private readonly PharmacyContext _context;

        #endregion

        #region Builders

        public MedicationControlRepository(PharmacyContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task NewControlAsync(MedicationControl medicationControl)
        {
            await _context.MedicationControls.AddAsync(new MedicationControl
            {
                Patient = await _context.Patients.FirstOrDefaultAsync(p => p.Name == medicationControl.Patient.Name),
                MedicineTaken = await _context.Medicines.FirstOrDefaultAsync(m => m.Name == medicationControl.MedicineTaken.Name)
            });
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
