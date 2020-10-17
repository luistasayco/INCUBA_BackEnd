using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
   public interface ITxRegistroEquipoDetalle1Repository : IRepositoryBase<BE_TxRegistroEquipoDetalle1>
    {
        Task<IEnumerable<BE_TxRegistroEquipoDetalle1>> GetAll(BE_General entidad);
        Task<BE_TxRegistroEquipoDetalle1> GetById(int id);
        Task<int> Create(BE_TxRegistroEquipoDetalle1 entidad);
        Task Update(BE_TxRegistroEquipoDetalle1 entidad);
        Task Delete(BE_TxRegistroEquipoDetalle1 entidad);
    }
}
