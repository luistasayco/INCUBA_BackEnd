using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IRequerimientoEquipoRepository : IRepositoryBase<BE_RequerimientoEquipo>
    {
        Task<IEnumerable<BE_RequerimientoEquipo>> GetAll(BE_RequerimientoEquipo entidad);
        Task<BE_RequerimientoEquipo> GetById(BE_RequerimientoEquipo entidad);
        Task<int> Create(BE_RequerimientoEquipo entidad);
        Task Update(BE_RequerimientoEquipo entidad);
        Task Delete(BE_RequerimientoEquipo entidad);
    }
}
