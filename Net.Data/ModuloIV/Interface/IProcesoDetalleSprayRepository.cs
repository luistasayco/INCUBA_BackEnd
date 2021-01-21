using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IProcesoDetalleSprayRepository : IRepositoryBase<BE_ProcesoDetalleSpray>
    {
        Task<IEnumerable<BE_ProcesoDetalleSpray>> GetAll(BE_ProcesoDetalleSpray entidad);
        Task<BE_ProcesoDetalleSpray> GetById(BE_ProcesoDetalleSpray entidad);
        Task<int> Create(BE_ProcesoDetalleSpray entidad);
        Task Update(BE_ProcesoDetalleSpray entidad);
        Task Delete(BE_ProcesoDetalleSpray entidad);
    }
}