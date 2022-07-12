using Pharmacy.src.dtos;
using System.Threading.Tasks;

namespace Pharmacy.src.repositories
{
    /// <summary>
    /// <para>Resumo: Responsávelpor representar os métodos de controle de medica~ção</para>
    /// <para>Crriado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 09/07/2022</para>
    /// </summary>
    public interface IMedicationControl
    {
        Task NewControlAsync(MedicationControlDTO medicationControl);
    }
}
