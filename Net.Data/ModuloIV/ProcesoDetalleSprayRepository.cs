using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class ProcesoDetalleSprayRepository : RepositoryBase<BE_ProcesoDetalleSpray>, IProcesoDetalleSprayRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "";

        const string SP_GET_ID = DB_ESQUEMA + "INC_GetProcesoDetalleSprayPorId";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetProcesoDetalleSprayInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetProcesoDetalleSprayDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetProcesoDetalleSprayUpdate";

        public ProcesoDetalleSprayRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_ProcesoDetalleSpray>> GetAll(BE_ProcesoDetalleSpray entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_ProcesoDetalleSpray> GetById(BE_ProcesoDetalleSpray entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_ProcesoDetalleSpray entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_ProcesoDetalleSpray entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_ProcesoDetalleSpray entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
