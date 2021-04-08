using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface ITxSIMRepository : IRepositoryBase<BE_TxSIM>
    {
        Task<IEnumerable<BE_TxSIM>> GetAll(FE_TxSIM entidad);
        Task<IEnumerable<BE_TxSIM>> GetByCodigoEmpresa(string codigoEmpresa);
        Task<BE_TxSIM> GetById(BE_TxSIM entidad);
        //Task<BE_TxSIM> GetByIdNew(BE_TxSIM entidad);
        Task<int> Create(BE_TxSIM entidad);
        Task Update(BE_TxSIM entidad);
        Task<BE_ResultadoTransaccion> UpdateStatus(BE_TxSIM entidad);
        Task Delete(BE_TxSIM entidad);
        Task<MemoryStream> GenerarPDF(BE_TxSIM entidad);
    }
}