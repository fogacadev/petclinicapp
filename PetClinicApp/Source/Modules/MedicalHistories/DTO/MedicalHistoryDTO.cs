using System.ComponentModel.DataAnnotations;

namespace PetClinicApp.Source.Modules.MedicalHistories.DTO
{
    public class MedicalHistoryDTO
    {
        public long Id { get; set; }
        public long HistoryTypeId { get; set; }
        [MinLength(3,ErrorMessage = "O titulo deve conter no mínimo 5 caracteres.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Description { get; set; }
        public long PetId { get; set; }
        public long? ClinicId { get; set; }
        public string Attachment { get; set; }
    }
}
