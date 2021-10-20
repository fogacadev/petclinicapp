namespace PetClinicApp.Source.Modules.MedicalHistories.DTO
{
    public class UpdateMedicalHistoryDTO
    {
        public long Id { get; set; }
        public long HistoryTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long? ClinicId { get; set; }
    }
}
