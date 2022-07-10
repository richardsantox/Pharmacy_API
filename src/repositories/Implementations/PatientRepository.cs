using Microsoft.EntityFrameworkCore;
using Pharmacy.src.context;
using Pharmacy.src.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.src.repositories.Implementations
{
    public class PatientRepository : IPatient
    {
        #region Attributes

        private readonly PharmacyContext _context;

        #endregion


        #region Builders
            
        public PatientRepository(PharmacyContext context)
        {
            _context = context;
        }

        #endregion


        #region Methods

        public async Task<List<MedicationControl>> GetAllMedicineTakensAsync(string name)
        {
            return await _context.MedicationControls
                .Include(c => c.Patient)
                .Include(c => c.MedicineTaken)
                .Where(c => c.Patient.Name == name)
                .ToListAsync();
        }

        public async Task<List<Patient>> GetAllPatiensAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task NewPatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(new Patient
            {
                Name = patient.Name
            });

            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
