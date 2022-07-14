using Microsoft.EntityFrameworkCore;
using Pharmacy.src.context;
using Pharmacy.src.dtos;
using Pharmacy.src.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.src.repositories.Implementations
{
    /// <summary>
    /// <para>Resumo: Classe responsável por implementar IMedicationControl</para>
    /// <para>Criado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 09/07/2022</para>
    /// </summary>
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

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar uma nova postagem</para>
        /// </summary>
        /// <param name="medicationControl">Construtor para cadastrar novo controle de medicação</param>
        /// <exception cref="Exception">Nome não pode ser nulo (ou está incorreto)</exception>
        public async Task NewControlAsync(MedicationControlDTO medicationControl)
        {
            if (!ExistPatient(medicationControl.Patient.Name)) throw new Exception("Nome do paciente não encontrado");
            if (!ExistMedicine(medicationControl.MedicineTaken.Name)) throw new Exception("Nome do Medicaemnto não encontrado");

            await _context.MedicationControls.AddAsync(new MedicationControl
            {
                Patient = await _context.Patients.FirstOrDefaultAsync(p => p.Name == medicationControl.Patient.Name),
                MedicineTaken = await _context.Medicines.FirstOrDefaultAsync(m => m.Name == medicationControl.MedicineTaken.Name)
            });
            await _context.SaveChangesAsync();


            bool ExistPatient (string name)
            {
                var aux = _context.Patients.FirstOrDefault(p => p.Name == name);
                return aux != null;
            }
            bool ExistMedicine(string name)
            {
                var aux = _context.Medicines.FirstOrDefault(p => p.Name == name);
                return aux != null;
            }
        }

        public async Task<IEnumerable> GetTheControlDataAsync()
        {
            var list = await _context.MedicationControls
                .Include(m => m.MedicineTaken)
                .Include(m => m.Patient)
                .ToListAsync();

            var result = list
                .GroupBy(u => u.Patient.Name)
                .Select(c => new 
                {
                    Name = c.Key,
                    Quantity = c.Count(),
                    Items = c.ToList()
                });

            return result;
        }

        #endregion
    }

}
