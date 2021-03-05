using Microsoft.AspNetCore.Http;
using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface ITxRegistroDocumentoRepository : IRepositoryBase<BE_TxRegistroDocumento>
    {
        Task<IEnumerable<BE_TxRegistroDocumento>> GetAll(BE_TxRegistroDocumento entidad);
        Task<IEnumerable<BE_GoogleDriveFiles>> GetAllEmpresaPorUsuario(BE_GoogleDriveFiles entidad);

        Task<IEnumerable<BE_GoogleDriveFiles>> GetGoogleDriveFilesPorId(BE_GoogleDriveFiles entidad);
        Task<BE_TxRegistroDocumento> GetById(BE_TxRegistroDocumento entidad);
        Task<BE_TxRegistroDocumento> GetByIdDocumento(BE_TxRegistroDocumento entidad);
        Task<BE_ResultadoTransaccion> Create(BE_TxRegistroDocumento entidad, IList<IFormFile> lista_anexo);
        Task<BE_ResultadoTransaccion> Update(BE_TxRegistroDocumento entidad);
        Task Delete(BE_TxRegistroDocumento entidad);
        Task<BE_MemoryStream> GetDownloadFileGoogleDrive(BE_TxRegistroDocumento entidad);
        Task<bool> GetUrlFileGoogleDrive(string id, string permissionValue, string userRule);
    }
}