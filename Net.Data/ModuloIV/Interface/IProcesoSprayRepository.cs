using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IProcesoSprayRepository : IRepositoryBase<BE_ProcesoSpray>
    {
        Task<IEnumerable<BE_ProcesoSpray>> GetAll(BE_ProcesoSpray entidad);
        Task<BE_ProcesoSpray> GetById(BE_ProcesoSpray entidad);
        Task<int> Create(BE_ProcesoSpray entidad);
        Task Update(BE_ProcesoSpray entidad);
        Task Delete(BE_ProcesoSpray entidad);
    }
}
