using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface ISubTipoExplotacionRepository : IRepositoryBase<BE_SubTipoExplotacion>
    {
        Task<IEnumerable<BE_SubTipoExplotacion>> GetAll(BE_SubTipoExplotacion entidad);
        Task<IEnumerable<BE_SubTipoExplotacion>> GetAllPorUsuario(EF_SubTipoExplotacion entidad);
        Task<BE_SubTipoExplotacion> GetById(BE_SubTipoExplotacion entidad);
        Task<int> Create(BE_SubTipoExplotacion entidad);
        Task Update(BE_SubTipoExplotacion entidad);
        Task Delete(BE_SubTipoExplotacion entidad);
    }
}