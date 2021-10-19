namespace PetClinicApp.Source.Shared.Uploads
{
    public class FileModel
    {

        public FileModel()
        {

        }

        public FileModel(byte[] buffer, string mimeType)
        {
            this.Buffer = buffer;
            this.MimeType = mimeType;
        }

        public byte[] Buffer { get; set; }
        public string MimeType { get; set; }
    }
}
