using Pharmacy.src.models;
using System.Threading.Tasks;

namespace Pharmacy.src.repositories
{
    public interface IMedicationControl
    {
        Task NewControlAsync(MedicationControl medicationControl);
    }
}
