using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface ITxRegistroEquipoRepository : IRepositoryBase<BE_TxRegistroEquipo>
    {
        Task<IEnumerable<BE_TxRegistroEquipo>> GetAll(BE_TxRegistroEquipo entidad);
        Task<BE_TxRegistroEquipo> GetNewObject(BE_General entidad);
        Task<BE_TxRegistroEquipo> GetById(BE_TxRegistroEquipo entidad);
        Task<int> Create(BE_TxRegistroEquipo entidad);
        Task Update(BE_TxRegistroEquipo entidad);
        Task UpdateStatus(BE_TxRegistroEquipo entidad);
        Task Delete(BE_TxRegistroEquipo entidad);
    }
}
