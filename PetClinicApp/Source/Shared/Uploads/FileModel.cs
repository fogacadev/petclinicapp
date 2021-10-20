using System.IO;

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

        public FileModel(byte[] buffer, string mimeType, string fileName)
        {
            this.Buffer = buffer;
            this.MimeType = mimeType;
            this.FileName = fileName;
        }

        public string FileName { get; set; }
        public byte[] Buffer { get; set; }
        public string MimeType { get; set; }

        public MemoryStream GetMemoryStream()
        {
            return new MemoryStream(this.Buffer);
        }
    }
}
