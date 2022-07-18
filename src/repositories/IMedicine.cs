using Pharmacy.src.dtos;
using Pharmacy.src.models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.src.repositories
{
    /// <summary>
    /// <para>Resumo: Responsávelpor representar os métodos de medicamentos</para>
    /// <para>Crriado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 09/07/2022</para>
    /// </summary>
    public interface IMedicine
    {
        Task NewMedicineAsync(MedicineDTO medicine);
        Task <Medicine> GetMedicineByNameAsync(string name);
        Task <List<Medicine>>GetAllMedicinesAsync();
        Task <IEnumerable> GetAllPatientsWhoTookAsync(string name);
        Task<IEnumerable> NumberPatientsWhoHaveAlreadyTakenTheDrugAsync();
    }
}
