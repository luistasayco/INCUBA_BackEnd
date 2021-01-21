using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class ProcesoSubCutaneaRepository : RepositoryBase<BE_ProcesoSubCutanea>, IProcesoSubCutaneaRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetProcesoSubCutaneaAll";

        const string SP_GET_ID = DB_ESQUEMA + "";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetProcesoSubCutaneaInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetProcesoSubCutaneaDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetProcesoSubCutaneaUpdate";

        public ProcesoSubCutaneaRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_ProcesoSubCutanea>> GetAll(BE_ProcesoSubCutanea entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_ProcesoSubCutanea> GetById(BE_ProcesoSubCutanea entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_ProcesoSubCutanea entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_ProcesoSubCutanea entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_ProcesoSubCutanea entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}