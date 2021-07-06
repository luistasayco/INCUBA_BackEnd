using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface ITxSINMIRepository : IRepositoryBase<BE_TxSINMI>
    {
        Task<IEnumerable<BE_TxSINMI>> GetAll(FE_TxSINMI entidad);
        Task<IEnumerable<BE_TxSINMIDetalle>> GetAllDetalleNew();
        Task<IEnumerable<BE_TxSINMI>> GetByCodigoEmpresa(string codigoEmpresa);
        Task<BE_TxSINMI> GetById(BE_TxSINMI entidad);
        Task<BE_ResultadoTransaccion> Create(BE_TxSINMI entidad);
        Task Update(BE_TxSINMI entidad);
        Task<BE_ResultadoTransaccion> UpdateStatus(BE_TxSINMI entidad);
        Task Delete(BE_TxSINMI entidad);
        Task<MemoryStream> GenerarPDF(BE_TxSINMI entidad);
    }
}
