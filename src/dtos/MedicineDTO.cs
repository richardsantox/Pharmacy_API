using System.ComponentModel.DataAnnotations;

namespace Pharmacy.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho para criar um novo medicamento</para>
    /// <para>Crriado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/07/2022</para>
    /// </summary>
    public class MedicineDTO
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        public MedicineDTO(string name)
        {
            Name = name;
        }
    }
}
