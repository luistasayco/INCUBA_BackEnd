using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface ICondicionLimpiezaRepository : IRepositoryBase<BE_CondicionLimpieza>
    {
        Task<IEnumerable<BE_CondicionLimpieza>> GetAll(BE_CondicionLimpieza entidad);
        Task<BE_CondicionLimpieza> GetById(BE_CondicionLimpieza entidad);
        Task<int> Create(BE_CondicionLimpieza entidad);
        Task Update(BE_CondicionLimpieza entidad);
        Task Delete(BE_CondicionLimpieza entidad);
    }
}
