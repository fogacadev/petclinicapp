namespace PetClinicApp.Source.Modules.MedicalHistories.DTO
{
    public class CreateMedicalHistoryDTO
    {
        public long HistoryTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long PetId { get; set; }
        public long? ClinicId { get; set; }
    }
}
