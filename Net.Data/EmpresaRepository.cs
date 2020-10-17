using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class EmpresaRepository : RepositoryBase<BE_Empresa>, IEmpresaRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET = DB_ESQUEMA + "INC_GetEmpresaAll";

        public EmpresaRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_Empresa>> GetAll(BE_Empresa entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
    }
}
