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
    public class MedicineRepository : IMedicine
    {
        /// <summary>
        /// <para>Resumo: Classe responsável por implementar IMedicine</para>
        /// <para>Criado por: Richard Santos</para>
        /// <para>Versão: 1.0</para>
        /// <para>Data: 09/07/2022</para>
        /// </summary>
        #region Attributes

        private readonly PharmacyContext _context;

        #endregion


        #region Builders

        public MedicineRepository(PharmacyContext context)
        {
            _context = context;
        }

        #endregion


        #region Methods

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todos os medicamentos</para>
        /// </summary>
        /// <return>Lista Medicine</return>
        public async Task<List<Medicine>> GetAllMedicinesAsync()
        {
            return await _context.Medicines.ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para buscar todos os remédios tomados por um paciente</para>
        /// </summary>
        /// <param name="name">string</param>
        public async Task<IEnumerable> GetAllPatientsWhoTookAsync(string name)
        {
            var list = await _context.MedicationControls
                .Include(c => c.Patient)
                .Include(c => c.MedicineTaken)
                .Where(c => c.MedicineTaken.Name == name)
                .ToListAsync();

            var patients = list
                .GroupBy(p => p.Patient.Name)
                .Select(s => s.Key );

            return patients;
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar Medicamento pelo nome</para>
        /// </summary>
        /// <param name="name">string</param>
        public async Task<Medicine> GetMedicineByNameAsync(string name)
        {
            return await _context.Medicines.FirstOrDefaultAsync(m => m.Name == name);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo medicamento</para>
        /// </summary>
        /// <param name="medicine">MedicineDTO</param>
        public async Task NewMedicineAsync(MedicineDTO medicine)
        {
            var user = await GetMedicineByNameAsync(medicine.Name);
            if (user != null) throw new Exception("Medicamento já existe no sistema");

            await _context.Medicines.AddAsync(new Medicine
            {
                Name = medicine.Name
            });

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono que retorna a quantidade de pacientes que tomaram cada medicamento</para>
        /// </summary>
        public async Task<IEnumerable> NumberPatientsWhoHaveAlreadyTakenTheDrugAsync()
        {
            var list = await _context.MedicationControls
                .Include(mc => mc.MedicineTaken)
                .Include(mc => mc.Patient)
                .ToListAsync();
            var num = list
                .GroupBy(p => p.MedicineTaken.Name)
                .Select(s => new
                {
                    Medicine = s.Key,
                    Quantity_Patient = s.Count(),
                });

            return num;
        }

        #endregion
    }
}
