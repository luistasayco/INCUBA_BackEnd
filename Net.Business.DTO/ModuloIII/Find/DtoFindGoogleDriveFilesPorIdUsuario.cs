using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindGoogleDriveFilesPorIdUsuario: EntityBase
    {
        public BE_GoogleDriveFiles RetornaGoogleDriveFiles()
        {
            return new BE_GoogleDriveFiles
            {
                RegUsuario = this.RegUsuario
            };
        }
    }
}
