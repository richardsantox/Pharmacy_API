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
    /// <para>Resumo: Classe responsável por implementar IPatient</para>
    /// <para>Criado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 09/07/2022</para>
    /// </summary>
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

        /// <summary>
        /// <para>Resumo: Método assíncrono que retorna a quantidade de medicação que cada paciente tomou</para>
        /// </summary>
        public async Task<IEnumerable> AmounMedicationPatientsWhoHaveAlreadyTakenAsync()
        {
            var list = await _context.MedicationControls
                .Include(mc => mc.MedicineTaken)
                .Include(mc => mc.Patient)
                .ToListAsync();
            var num = list
                .GroupBy(p => p.Patient.Name)
                .Select(s => new
                {
                    Patient = s.Key,
                    Quantity_Medicine = s.Count(),
                });

            return num;
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para buscar todos os pacientes que tomaram um remédio</para>
        /// </summary>
        /// <param name="name">string</param>
        public async Task<List<MedicationControl>> GetAllMedicineTakensAsync(string name)
        {
            return await _context.MedicationControls
              .Include(c => c.Patient)
              .Include(c => c.MedicineTaken)
              .Where(c => c.Patient.Name == name)
              .ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todos os patient</para>
        /// </summary>
        /// <return>Lista Patient</return>
        public async Task<List<Patient>> GetAllPatiensAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo paciente</para>
        /// </summary>
        /// <param name="patient">PatientDTO</param>
        public async Task NewPatientAsync(PatientDTO patient)
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
