using System;

namespace Net.Business.Entities
{
    public class BE_GoogleDriveFiles: EntityBase
    {
        public string CodigoEmpresa { get; set; }
        public string IdGoogleDrive { get; set; }
        public string Names { get; set; }
        public long? Size { get; set; }
        public long? Version { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string MimeType { get; set; }
    }
}
