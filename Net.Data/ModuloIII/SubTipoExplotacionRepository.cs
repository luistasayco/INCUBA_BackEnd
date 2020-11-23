using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class SubTipoExplotacionRepository : RepositoryBase<BE_SubTipoExplotacion>, ISubTipoExplotacionRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "EXT_Get_SubTipoExplotacionPorId";
        const string SP_GET_ID = DB_ESQUEMA + "EXT_Get_SubTipoExplotacionPorId";
        const string SP_GET_POR_USUARIO = DB_ESQUEMA + "EXT_Get_SubTipoExplotacionPorIdUsuario";
        const string SP_INSERT = DB_ESQUEMA + "EXT_SetSubTipoExplotacionInsert";
        const string SP_DELETE = DB_ESQUEMA + "EXT_SetSubTipoExplotacionDelete";
        const string SP_UPDATE = DB_ESQUEMA + "EXT_SetSubTipoExplotacionUpdate";

        public SubTipoExplotacionRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_SubTipoExplotacion>> GetAll(BE_SubTipoExplotacion entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<IEnumerable<BE_SubTipoExplotacion>> GetAllPorUsuario(EF_SubTipoExplotacion entidad)
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_SubTipoExplotacion>(SP_GET_POR_USUARIO, entidad));
        }
        public Task<BE_SubTipoExplotacion> GetById(BE_SubTipoExplotacion entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_SubTipoExplotacion entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_SubTipoExplotacion entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_SubTipoExplotacion entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}