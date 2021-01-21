using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class VacunaRepository : RepositoryBase<BE_Vacuna>, IVacunaRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetVacunaAll";

        const string SP_GET_ID = DB_ESQUEMA + "";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetVacunaInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetVacunaDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetVacunaUpdate";

        public VacunaRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_Vacuna>> GetAll(BE_Vacuna entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_Vacuna> GetById(BE_Vacuna entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_Vacuna entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_Vacuna entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_Vacuna entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
