using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface ITxSINMIConsolidadoRepository : IRepositoryBase<BE_TxSINMIConsolidado>
    {
        Task<IEnumerable<BE_TxSINMIConsolidado>> GetAll(FE_TxSINMIConsolidado entidad);
        Task<BE_TxSINMIConsolidado> GetById(BE_TxSINMIConsolidado entidad);
        Task<BE_ResultadoTransaccion> Create(BE_TxSINMIConsolidado entidad);
        Task Update(BE_TxSINMIConsolidado entidad);
        Task<BE_ResultadoTransaccion> UpdateStatus(BE_TxSINMIConsolidado entidad);
        Task Delete(BE_TxSINMIConsolidado entidad);
        Task<MemoryStream> GenerarPDF(int id, string descripcionEmpresa);
    }
}
