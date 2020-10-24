using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class CalidadRepository : RepositoryBase<BE_Calidad>, ICalidadRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetCalidadAll";
        const string SP_GET_ID = DB_ESQUEMA + "INC_GetCalidadAll";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetCalidadInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetCalidadDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetCalidadUpdate";

        public CalidadRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_Calidad>> GetAll(BE_Calidad entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_Calidad> GetById(BE_Calidad entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_Calidad entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_Calidad entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_Calidad entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
