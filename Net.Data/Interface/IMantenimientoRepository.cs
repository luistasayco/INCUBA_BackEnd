using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IMantenimientoRepository : IRepositoryBase<BE_Mantenimiento>
    {
        Task<IEnumerable<BE_Mantenimiento>> GetAll(BE_Mantenimiento entidad);
        Task<BE_Mantenimiento> GetById(BE_Mantenimiento entidad);
        Task<int> Create(BE_Mantenimiento entidad);
        Task Update(BE_Mantenimiento entidad);
        Task Delete(BE_Mantenimiento entidad);
    }
}
