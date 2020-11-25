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
        Task<BE_TxRegistroDocumento> GetById(BE_TxRegistroDocumento entidad);
        Task<int> Create(BE_TxRegistroDocumento entidad, IList<IFormFile> lista_anexo);
        Task Delete(BE_TxRegistroDocumento entidad);
        Task<BE_MemoryStream> GetDownloadFileGoogleDrive(BE_TxRegistroDocumento entidad);
    }
}