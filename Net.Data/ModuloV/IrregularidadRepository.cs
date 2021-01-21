using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class IrregularidadRepository : RepositoryBase<BE_Irregularidad>, IIrregularidadRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetIrregularidadAll";

        const string SP_GET_ID = DB_ESQUEMA + "";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetIrregularidadInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetIrregularidadDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetIrregularidadUpdate";

        public IrregularidadRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_Irregularidad>> GetAll(BE_Irregularidad entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_Irregularidad> GetById(BE_Irregularidad entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_Irregularidad entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_Irregularidad entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_Irregularidad entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}