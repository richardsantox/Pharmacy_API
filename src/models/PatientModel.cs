using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pharmacy.src.models
{
    /// <summary>
    /// <para>Resumo: Classe responsásvel por representar tb_patient no banco.</para>
    /// <para>Criado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 09/07/2022</para>
    /// </summary>
    [Table("tb_patient")]
    public class Patient
    {
        #region Attributes

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [JsonIgnore, InverseProperty("Patient")]
        public List<MedicationControl> MyMedicines { get; set; }

        #endregion
    }
}
