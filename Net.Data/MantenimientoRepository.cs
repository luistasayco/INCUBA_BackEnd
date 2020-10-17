using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class MantenimientoRepository : RepositoryBase<BE_Mantenimiento>, IMantenimientoRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetMantenimientoAll";
        const string SP_GET_ID = DB_ESQUEMA + "INC_GetMantenimientoAll";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetMantenimientoInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetMantenimientoDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetMantenimientoUpdate";

        public MantenimientoRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_Mantenimiento>> GetAll(BE_Mantenimiento entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_Mantenimiento> GetById(BE_Mantenimiento entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_Mantenimiento entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_Mantenimiento entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_Mantenimiento entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
