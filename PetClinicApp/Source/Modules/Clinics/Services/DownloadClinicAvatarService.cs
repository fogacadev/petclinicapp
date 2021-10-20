using PetClinicApp.Source.Modules.Clinics.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Uploads;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.Services
{
    public class DownloadClinicAvatarService
    {
        private readonly IClinicsRepository clinicsRepository;
        public DownloadClinicAvatarService(IClinicsRepository clinicsRepository)
        {
            this.clinicsRepository = clinicsRepository;
        }

        public async Task<FileModel> ExecuteAsync(long id)
        {
            var clinic = await clinicsRepository.Find(id);

            if (clinic == null)
            {
                throw new AppErrorException("Avatar does not exists", HttpStatusCode.NotFound);
            }

            var file = await UploadFile.Download("clinic_avatar", clinic.Avatar);

            if (file == null)
            {
                throw new AppErrorException("Avatar does not exists.", HttpStatusCode.NotFound);
            }
            return file;
        }
    }
}
