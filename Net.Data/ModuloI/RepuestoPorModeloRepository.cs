using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class RepuestoPorModeloRepository : RepositoryBase<BE_RepuestoPorModelo>, IRepuestoPorModeloRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_ALL = DB_ESQUEMA + "INC_GetRepuestoPorModeloAll";
        const string SP_GET_ALL_XML = DB_ESQUEMA + "INC_GetRepuestoSeleccionadoPorModeloXml";
        const string SP_GET_SELECCIONADO = DB_ESQUEMA + "INC_GetRepuestoSeleccionadoPorModelo";
        const string SP_GET_POR_SELECCIONAR = DB_ESQUEMA + "INC_GetRepuestoPorSeleccionarPorModelo";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetRepuestoPorModeloInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetRepuestoPorModeloDelete";

        public RepuestoPorModeloRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_RepuestoPorModelo>> GetAll(BE_RepuestoPorModelo entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET_ALL));
        }
        public Task<IEnumerable<BE_RepuestoPorModelo>> GetAllSeleccionado(BE_RepuestoPorModelo entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET_SELECCIONADO));
        }
        public Task<IEnumerable<BE_RepuestoPorModelo>> GetXmlSeleccionado(BE_Xml entidad)
        {
            //return Task.Run(() => FindAll(entidad, SP_GET_ALL_XML));
            return Task.Run(() => context.ExecuteSqlViewFindByCondition<BE_RepuestoPorModelo>(SP_GET_ALL_XML, entidad));
        }
        public Task<IEnumerable<BE_RepuestoPorModelo>> GetAllPorSeleccionar(BE_RepuestoPorModelo entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET_POR_SELECCIONAR));
        }
        public async Task<int> Create(BE_RepuestoPorModelo entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Delete(BE_RepuestoPorModelo entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
