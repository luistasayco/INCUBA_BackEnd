using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class OrganoDetalleRepository : RepositoryBase<BE_OrganoDetalle>, IOrganoDetalleRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetOrganoDetallePorId";
        const string SP_GET_ID = DB_ESQUEMA + "INC_GetOrganoDetallePorId";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetOrganoDetalleInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetOrganoDetalleDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetOrganoDetalleUpdate";

        public OrganoDetalleRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_OrganoDetalle>> GetAll(BE_OrganoDetalle entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_OrganoDetalle> GetById(BE_OrganoDetalle entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_OrganoDetalle entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_OrganoDetalle entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_OrganoDetalle entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
