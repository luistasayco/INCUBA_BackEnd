using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class IndiceEficienciaRepository : RepositoryBase<BE_IndiceEficiencia>, IIndiceEficienciaRepository
    {
        const string DB_ESQUEMA = "";
        const string SP_GET = DB_ESQUEMA + "INC_GetIndiceEficienciaAll";

        const string SP_GET_ID = DB_ESQUEMA + "";
        const string SP_INSERT = DB_ESQUEMA + "";
        const string SP_DELETE = DB_ESQUEMA + "";
        const string SP_UPDATE = DB_ESQUEMA + "";

        public IndiceEficienciaRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_IndiceEficiencia>> GetAll(BE_IndiceEficiencia entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_IndiceEficiencia> GetById(BE_IndiceEficiencia entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_IndiceEficiencia entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_IndiceEficiencia entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_IndiceEficiencia entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
