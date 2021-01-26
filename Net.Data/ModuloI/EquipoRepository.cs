using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class EquipoRepository : RepositoryBase<BE_Equipo>, IEquipoRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetEquipoAll";
        const string SP_GET_POR_FILTROS = DB_ESQUEMA + "INC_GetEquipoPorFiltro";

        public EquipoRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_Equipo>> GetAll(BE_General entidad)
        { 
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_Equipo>(SP_GET, entidad));
        }

        public Task<IEnumerable<BE_Equipo>> GetAllPorFiltros(BE_General entidad)
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_Equipo>(SP_GET_POR_FILTROS, entidad));
        }
    }
}
