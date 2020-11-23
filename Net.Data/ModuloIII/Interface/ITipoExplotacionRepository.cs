using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface ITipoExplotacionRepository : IRepositoryBase<BE_TipoExplotacion>
    {
        Task<IEnumerable<BE_TipoExplotacion>> GetAll(BE_TipoExplotacion entidad);
        Task<IEnumerable<BE_TipoExplotacion>> GetAllPorUsuario(EF_TipoExplotacion entidad);
        Task<BE_TipoExplotacion> GetById(BE_TipoExplotacion entidad);
        Task<int> Create(BE_TipoExplotacion entidad);
        Task Update(BE_TipoExplotacion entidad);
        Task Delete(BE_TipoExplotacion entidad);
    }
}
