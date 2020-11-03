using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IProcesoDetalleRepository:IRepositoryBase<BE_ProcesoDetalle>
    {

        Task<IEnumerable<BE_ProcesoDetalle>> GetAll(BE_ProcesoDetalle entidad);
        Task<BE_ProcesoDetalle> GetById(BE_ProcesoDetalle entidad);
        Task<int> Create(BE_ProcesoDetalle entidad);
        Task Update(BE_ProcesoDetalle entidad);
        Task Delete(BE_ProcesoDetalle entidad);
    }
}
