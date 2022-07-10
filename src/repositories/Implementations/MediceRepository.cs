using Microsoft.EntityFrameworkCore;
using Pharmacy.src.context;
using Pharmacy.src.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.src.repositories.Implementations
{
    public class MediceRepository : IMedicine
    {
        #region Attributes

        private readonly PharmacyContext _context;

        #endregion


        #region Builders

        public MediceRepository(PharmacyContext context)
        {
            _context = context;
        }
        
        #endregion


        #region Methods

        public async Task<List<Medicine>> GetAllMedicinesAsync()
        {
            return await _context.Medicines.ToListAsync();
        }

        public async Task<List<MedicationControl>> GetAllPatientsWhoTookAsync(string name)
        {
            return await _context.MedicationControls
                .Include(c => c.Patient)
                .Include(c => c.MedicineTaken)
                .Where(c => c.MedicineTaken.Name == name)
                .ToListAsync();
        }

        public async Task NewMedicineAsync(Medicine medicine)
        {
            await _context.Medicines.AddAsync(new Medicine
            {
                Name = medicine.Name
            });

            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
