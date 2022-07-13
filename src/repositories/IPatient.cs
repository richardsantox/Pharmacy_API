using Pharmacy.src.dtos;
using Pharmacy.src.models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.src.repositories
{
    /// <summary>
    /// <para>Resumo: Responsávelpor representar os métodos de paciente</para>
    /// <para>Crriado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 09/07/2022</para>
    /// </summary>
    public interface IPatient
    {
        Task NewPatientAsync(PatientDTO patient);
        Task <List<Patient>>GetAllPatiensAsync();
        Task <List<MedicationControl>> GetAllMedicineTakensAsync(string name);
        Task<IEnumerable> AmounMedicationPatientsWhoHaveAlreadyTakenAsync();
    }
}
