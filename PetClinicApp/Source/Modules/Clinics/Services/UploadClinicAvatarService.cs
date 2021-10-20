using Microsoft.AspNetCore.Http;
using PetClinicApp.Source.Modules.Clinics.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Uploads;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.Services
{
    public class UploadClinicAvatarService
    {
        private readonly IClinicsRepository clinicsRepository;
        public UploadClinicAvatarService(IClinicsRepository clinicsRepository)
        {
            this.clinicsRepository = clinicsRepository;
        }

        public async Task ExecuteAsync(long clinicId, IFormFile file)
        {
            var clinic = await clinicsRepository.Find(clinicId);

            if (clinic == null)
            {
                throw new AppErrorException("Clinic does not exists", HttpStatusCode.NotFound);
            }

            var newFileName = await UploadFile.Upload("clinic_avatar", file, clinic.Avatar);

            clinic.Avatar = newFileName;
            await clinicsRepository.Update(clinic);
        }
    }
}
