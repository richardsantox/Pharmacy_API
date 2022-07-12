namespace Pharmacy.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho para criar um novo contrtole de medicação</para>
    /// <para>Crriado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/07/2022</para>
    /// </summary>
    public class MedicationControlDTO
    {
        public PatientDTO Patient { get; set; }
        public MedicineDTO MedicineTaken { get; set; }

        public MedicationControlDTO(PatientDTO patient, MedicineDTO medicineTaken)
        {
            Patient = patient;
            MedicineTaken = medicineTaken;
        }
    }
}
