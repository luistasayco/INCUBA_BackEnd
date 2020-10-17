using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class ProcesoDetalleRepository : RepositoryBase<BE_ProcesoDetalle>, IProcesoDetalleRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetProcesoDetallePorId";
        const string SP_GET_ID = DB_ESQUEMA + "INC_GetProcesoDetallePorId";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetProcesoDetalleInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetProcesoDetalleDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetProcesoDetalleUpdate";

        public ProcesoDetalleRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_ProcesoDetalle>> GetAll(BE_ProcesoDetalle entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_ProcesoDetalle> GetById(BE_ProcesoDetalle entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_ProcesoDetalle entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_ProcesoDetalle entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_ProcesoDetalle entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
