using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class MantenimientoPorModeloRepository : RepositoryBase<BE_MantenimientoPorModelo>, IMantenimientoPorModeloRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_ALL = DB_ESQUEMA + "INC_GetMantenimientoPorModeloAll";
        const string SP_GET_SELECCIONADO = DB_ESQUEMA + "INC_GetMantenimientoSeleccionadoPorModelo";
        const string SP_GET_POR_SELECCIONADO = DB_ESQUEMA + "INC_GetMantenimientoPorSeleccionarPorModelo";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetMantenimientoPorModeloInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetMantenimientoPorModeloDelete";

        public MantenimientoPorModeloRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_MantenimientoPorModelo>> GetAll(BE_MantenimientoPorModelo entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET_ALL));
        }
        public Task<IEnumerable<BE_MantenimientoPorModelo>> GetAllSeleccionado(BE_MantenimientoPorModelo entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET_SELECCIONADO));
        }
        public Task<IEnumerable<BE_MantenimientoPorModelo>> GetAllPorSeleccionar(BE_MantenimientoPorModelo entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET_POR_SELECCIONADO));
        }
        public async Task<int> Create(BE_MantenimientoPorModelo entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Delete(BE_MantenimientoPorModelo entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
