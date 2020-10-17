using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class TxRegistroEquipoDetalle1Repository : RepositoryBase<BE_TxRegistroEquipoDetalle1>, ITxRegistroEquipoDetalle1Repository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_POR_FILTRO = DB_ESQUEMA + "INC_GetTxRegistroEquipoDetalle1PorFiltro";
        const string SP_GET_ID = DB_ESQUEMA + "INC_GetCalidadAll";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetCalidadInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetCalidadDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetCalidadUpdate";

        public TxRegistroEquipoDetalle1Repository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_TxRegistroEquipoDetalle1>> GetAll(BE_General entidad)
        {
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_TxRegistroEquipoDetalle1>(SP_GET_POR_FILTRO, entidad));
        }
        public Task<BE_TxRegistroEquipoDetalle1> GetById(int id)
        {
            return Task.Run(() => FindById(new BE_TxRegistroEquipoDetalle1 { IdRegistroEquipo = id }, SP_GET_ID));
        }
        public async Task<int> Create(BE_TxRegistroEquipoDetalle1 entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_TxRegistroEquipoDetalle1 entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_TxRegistroEquipoDetalle1 entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
