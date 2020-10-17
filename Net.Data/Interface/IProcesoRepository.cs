using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IProcesoRepository : IRepositoryBase<BE_Proceso>
    {
        Task<IEnumerable<BE_Proceso>> GetAll(BE_Proceso entidad);
        Task<BE_Proceso> GetById(BE_Proceso entidad);
        Task<int> Create(BE_Proceso entidad);
        Task Update(BE_Proceso entidad);
        Task Delete(BE_Proceso entidad);
    }
}
