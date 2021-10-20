using PetClinicApp.Source.Modules.MedicalHistories.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Uploads;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Services
{
    public class DeleteHistoryAttachmentService
    {
        private readonly IMedicalHistoriesRepository medicalHistoriesRepository;
        public DeleteHistoryAttachmentService(IMedicalHistoriesRepository medicalHistoriesRepository)
        {
            this.medicalHistoriesRepository = medicalHistoriesRepository;
        }

        public async Task ExecuteAsync(long loggedUserId, long id)
        {
            var history = await medicalHistoriesRepository.Find(id);

            if(history == null)
            {
                throw new AppErrorException("Attachment does not exists.", HttpStatusCode.NotFound);
            }

            if (string.IsNullOrEmpty(history.Attachment))
            {
                return;
            }

            UploadFile.Delete("history_attachment", history.Attachment);

            history.Attachment = "";
            await medicalHistoriesRepository.Update(history);
        }
    }
}
