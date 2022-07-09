using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.src.models
{
    [Table("tb_medication_control")]
    public class MedicationControl
    {
        #region Attributes

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int NumberOfTimes { get; set; }

        [ForeignKey("fk_medicine")]
        public Medicine MedicineTaken { get; set; }

        [ForeignKey("fk_patient")]
        public Patient Patient { get; set; }

        #endregion
    }
}
