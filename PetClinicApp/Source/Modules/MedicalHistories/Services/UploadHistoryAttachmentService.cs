using Microsoft.AspNetCore.Http;
using MimeTypes;
using PetClinicApp.Source.Modules.MedicalHistories.Repositories;
using PetClinicApp.Source.Shared.Errors;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Services
{
    public class UploadHistoryAttachmentService
    {
        private readonly IMedicalHistoriesRepository medicalHistoriesRepository;
        public UploadHistoryAttachmentService(IMedicalHistoriesRepository medicalHistoriesRepository)
        {
            this.medicalHistoriesRepository = medicalHistoriesRepository;
        }

        public async Task ExecuteAsync(long loggedUserId, long historyId, IFormFile file)
        {
             var history = await medicalHistoriesRepository.Find(historyId);

            if(history == null)
            {
                throw new AppErrorException("Attachment does not exists", HttpStatusCode.NotFound);
            }
            
            var path = "files\\history_attachment\\";


            //apaga o anexo antigo
            if (!string.IsNullOrEmpty(history.Attachment))
            {
                if (File.Exists($"{path}{history.Attachment}"))
                {
                    File.Delete($"{path}{history.Attachment}");
                }
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var extension = file.Name.Substring(file.Name.LastIndexOf('.'));
            var newFileName = Path.GetRandomFileName();
            newFileName = newFileName + extension;


            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                await File.WriteAllBytesAsync($"{path}{newFileName}", ms.GetBuffer());
            }

            

            history.Attachment = newFileName;
            await medicalHistoriesRepository.Update(history);
        }
    }
}
