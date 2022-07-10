using Pharmacy.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.src.repositories
{
    public interface IPatient
    {
        Task NewPatientAsync(Patient patient);
        Task <List<Patient>>GetAllPatiensAsync();
        Task <List<MedicationControl>>GetAllMedicineTakensAsync(string name);
    }
}
