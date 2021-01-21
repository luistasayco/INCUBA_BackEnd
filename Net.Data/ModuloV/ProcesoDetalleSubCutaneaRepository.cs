using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class ProcesoDetalleSubCutaneaRepository : RepositoryBase<BE_ProcesoDetalleSubCutanea>, IProcesoDetalleSubCutaneaRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "";

        const string SP_GET_ID = DB_ESQUEMA + "INC_GetProcesoDetalleSubCutaneaPorId";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetProcesoDetalleSubCutaneaInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetProcesoDetalleSubCutaneaDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetProcesoDetalleSubCutaneaUpdate";

        public ProcesoDetalleSubCutaneaRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_ProcesoDetalleSubCutanea>> GetAll(BE_ProcesoDetalleSubCutanea entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_ProcesoDetalleSubCutanea> GetById(BE_ProcesoDetalleSubCutanea entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_ProcesoDetalleSubCutanea entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_ProcesoDetalleSubCutanea entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_ProcesoDetalleSubCutanea entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}