using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IVacunaRepository : IRepositoryBase<BE_Vacuna>
    {
        Task<IEnumerable<BE_Vacuna>> GetAll(BE_Vacuna entidad);
        Task<BE_Vacuna> GetById(BE_Vacuna entidad);
        Task<int> Create(BE_Vacuna entidad);
        Task Update(BE_Vacuna entidad);
        Task Delete(BE_Vacuna entidad);
    }
}
