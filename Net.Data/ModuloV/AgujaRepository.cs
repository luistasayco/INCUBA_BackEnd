using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class AgujaRepository : RepositoryBase<BE_Aguja>, IAgujaRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetAgujaAll";

        const string SP_GET_ID = DB_ESQUEMA + "";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetAgujaInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetAgujaDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetAgujaUpdate";

        public AgujaRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_Aguja>> GetAll(BE_Aguja entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_Aguja> GetById(BE_Aguja entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_Aguja entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_Aguja entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_Aguja entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}