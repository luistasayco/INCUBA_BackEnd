using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class RequerimientoEquipoRepository : RepositoryBase<BE_RequerimientoEquipo>, IRequerimientoEquipoRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetRequerimientoEquipoAll";
        const string SP_GET_ID = DB_ESQUEMA + "INC_GetRequerimientoEquipoAll";
        const string SP_INSERT = DB_ESQUEMA + "INC_SetRequerimientoEquipoInsert";
        const string SP_DELETE = DB_ESQUEMA + "INC_SetRequerimientoEquipoDelete";
        const string SP_UPDATE = DB_ESQUEMA + "INC_SetRequerimientoEquipoUpdate";

        public RequerimientoEquipoRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_RequerimientoEquipo>> GetAll(BE_RequerimientoEquipo entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_RequerimientoEquipo> GetById(BE_RequerimientoEquipo entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_RequerimientoEquipo entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_RequerimientoEquipo entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_RequerimientoEquipo entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
