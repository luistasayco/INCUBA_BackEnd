using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class ProcesoSprayRepository : RepositoryBase<BE_ProcesoSpray>, IProcesoSprayRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetProcesoSprayAll";

        const string SP_GET_ID = DB_ESQUEMA + "";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetProcesoSprayInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetProcesoSprayUpdate";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetProcesoSprayDelete";

        public ProcesoSprayRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_ProcesoSpray>> GetAll(BE_ProcesoSpray entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_ProcesoSpray> GetById(BE_ProcesoSpray entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_ProcesoSpray entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_ProcesoSpray entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_ProcesoSpray entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
