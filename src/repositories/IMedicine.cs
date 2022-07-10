using Pharmacy.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.src.repositories
{
    public interface IMedicine
    {
        Task NewMedicineAsync(Medicine medicine);
        Task <List<Medicine>>GetAllMedicinesAsync();
        Task <List<MedicationControl>>GetAllPatientsWhoTookAsync(string name);
    }
}
