using Microsoft.AspNetCore.Mvc;
using MimeTypes;
using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using PetClinicApp.Source.Modules.MedicalHistories.Repositories;
using PetClinicApp.Source.Shared.Errors;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.MedicalHistories.Services
{
    public class DownloadHistoryAttachmentService
    {
        private readonly IMedicalHistoriesRepository medicalHistoriesRepository;
        public DownloadHistoryAttachmentService(IMedicalHistoriesRepository medicalHistoriesRepository)
        {
            this.medicalHistoriesRepository = medicalHistoriesRepository;
        }

        //FileContentResult
        //new FileContentResult(attachment, MediaTypeHeaderValue.Parse("application/octet-stream"));
        public async Task<FileContentResult> ExecuteAsync(long loggedUserId, long id)
        {
            var history = await medicalHistoriesRepository.Find(id);

            if (history == null)
            {
                throw new AppErrorException("Attachment does not exists", HttpStatusCode.NotFound);
            }

            var path = "files\\history_attachment\\";
            var filename = $"{path}{history.Attachment}";

            //apaga o anexo antigo
            if (!string.IsNullOrEmpty(history.Attachment))
            {
                if (File.Exists(filename))
                {
                    var extension = history.Attachment.Substring(history.Attachment.LastIndexOf('.'));
                    extension = extension.Remove(0, 1);
                    var mimeType = MimeTypeMap.GetMimeType(extension);

                    var bytes = await File.ReadAllBytesAsync(filename);
                    return new FileContentResult(bytes, mimeType);
                }
            }

            throw new AppErrorException("Attachment does not exists", HttpStatusCode.NotFound);
        }
    }
}
