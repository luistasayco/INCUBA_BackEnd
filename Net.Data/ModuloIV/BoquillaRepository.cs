using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class BoquillaRepository : RepositoryBase<BE_Boquilla>, IBoquillaRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetBoquillaAll";

        const string SP_GET_ID = DB_ESQUEMA + "";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetBoquillaInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetBoquillaDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetBoquillaUpdate";

        public BoquillaRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_Boquilla>> GetAll(BE_Boquilla entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_Boquilla> GetById(BE_Boquilla entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_Boquilla entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_Boquilla entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_Boquilla entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
