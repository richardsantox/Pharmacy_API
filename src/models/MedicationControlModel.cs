using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.src.models
{
    /// <summary>
    /// <para>Resumo: Classe responsável por representar tb_medication_control no banco.</para>
    /// <para>Criado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 09/07/2022</para>
    /// </summary>
    [Table("tb_medication_control")]
    public class MedicationControl
    {
        #region Attributes

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("fk_medicine")]
        public Medicine MedicineTaken { get; set; }

        [ForeignKey("fk_patient")]
        public Patient Patient { get; set; }

        #endregion
    }
}
