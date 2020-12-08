using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindGoogleDriveFiles
    {
        public string IdGoogleDrive { get; set; }
        public BE_GoogleDriveFiles RetornaGoogleDriveFiles()
        {
            return new BE_GoogleDriveFiles
            {
                IdGoogleDrive = this.IdGoogleDrive
            };
        }
    }
}
