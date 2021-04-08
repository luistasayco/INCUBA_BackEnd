using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface ITxSIMConsolidadoRepository : IRepositoryBase<BE_TxSIMConsolidado>
    {
        Task<IEnumerable<BE_TxSIMConsolidado>> GetAll(FE_TxSIMConsolidado entidad);
        Task<BE_TxSIMConsolidado> GetById(BE_TxSIMConsolidado entidad);
        Task<int> Create(BE_TxSIMConsolidado entidad);
        Task Update(BE_TxSIMConsolidado entidad);
        Task<BE_ResultadoTransaccion> UpdateStatus(BE_TxSIMConsolidado entidad);
        Task Delete(BE_TxSIMConsolidado entidad);
        Task<MemoryStream> GenerarPDF(int id, string descripcionEmpresa);
    }
}
