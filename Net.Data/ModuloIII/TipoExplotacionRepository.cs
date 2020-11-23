using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class TipoExplotacionRepository : RepositoryBase<BE_TipoExplotacion>, ITipoExplotacionRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_ID = "";
        const string SP_GET = DB_ESQUEMA + "EXT_GetTipoExplotacionAll";
        const string SP_GET_POR_USUARIO = DB_ESQUEMA + "EXT_GetTipoExplotacionPorIdUsuario";
        const string SP_INSERT = DB_ESQUEMA + "EXT_SetTipoExplotacionInsert";
        const string SP_DELETE = DB_ESQUEMA + "EXT_SetTipoExplotacionDelete";
        const string SP_UPDATE = DB_ESQUEMA + "EXT_SetTipoExplotacionUpdate";

        public TipoExplotacionRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_TipoExplotacion>> GetAll(BE_TipoExplotacion entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<IEnumerable<BE_TipoExplotacion>> GetAllPorUsuario(EF_TipoExplotacion entidad)
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_TipoExplotacion>(SP_GET_POR_USUARIO, entidad));
        }

        public Task<BE_TipoExplotacion> GetById(BE_TipoExplotacion entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_TipoExplotacion entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_TipoExplotacion entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_TipoExplotacion entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
