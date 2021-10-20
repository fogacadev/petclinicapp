using Microsoft.AspNetCore.Http;
using MimeTypes;
using PetClinicApp.Source.Modules.MedicalHistories.Repositories;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Uploads;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Services
{
    public class UploadHistoryAttachmentService
    {
        private readonly IMedicalHistoriesRepository medicalHistoriesRepository;
        private readonly IPetsRepository petsRepository;
        public UploadHistoryAttachmentService(IMedicalHistoriesRepository medicalHistoriesRepository,
            IPetsRepository petsRepository)
        {
            this.medicalHistoriesRepository = medicalHistoriesRepository;
            this.petsRepository = petsRepository;
        }

        public async Task ExecuteAsync(long loggedUserId, long historyId, IFormFile file)
        {
             var history = await medicalHistoriesRepository.Find(historyId);

            if(history == null)
            {
                throw new AppErrorException("Attachment does not exists", HttpStatusCode.NotFound);
            }

            var pet = await petsRepository.Find(history.PetId);
            if(pet.UserId != loggedUserId)
            {
                throw new AppErrorException("You are not allowed to perform this action", HttpStatusCode.Forbidden);
            }

            var newFileName = await UploadFile.Upload("history_attachment", file, history.Attachment);

            history.Attachment = newFileName;
            await medicalHistoriesRepository.Update(history);
        }
    }
}
