using Microsoft.AspNetCore.Mvc;
using MimeTypes;
using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using PetClinicApp.Source.Modules.MedicalHistories.Repositories;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Uploads;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Services
{
    public class DownloadHistoryAttachmentService
    {
        private readonly IMedicalHistoriesRepository medicalHistoriesRepository;
        private readonly IPetsRepository petsRepository;
        public DownloadHistoryAttachmentService(IMedicalHistoriesRepository medicalHistoriesRepository,
            IPetsRepository petsRepository)
        {
            this.medicalHistoriesRepository = medicalHistoriesRepository;
            this.petsRepository = petsRepository;
        }

        //FileContentResult
        //new FileContentResult(attachment, MediaTypeHeaderValue.Parse("application/octet-stream"));
        public async Task<FileModel> ExecuteAsync(long loggedUserId, long id)
        {
            var history = await medicalHistoriesRepository.Find(id);
            if (history == null)
            {
                throw new AppErrorException("Attachment does not exists", HttpStatusCode.NotFound);
            }


            var pet = await petsRepository.Find(history.PetId);

            if(pet.UserId != loggedUserId)
            {
                throw new AppErrorException("You are not allowed to perform this action", HttpStatusCode.Forbidden);
            }

            var file = await UploadFile.Download("history_attachment", history.Attachment);
            if(file == null)
            {
                throw new AppErrorException("Attachment does not exists", HttpStatusCode.NotFound);
            }

            return file;
        }
    }
}
