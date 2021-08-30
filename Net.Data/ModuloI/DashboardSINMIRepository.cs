using System.Collections.Generic;
using Net.Connection;
using Net.Business.Entities;
using System.Threading.Tasks;

namespace Net.Data
{
    public class DashboardSINMIRepository : RepositoryBase<BE_DashboardSINMI>, IDashboardSINMIRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_ALL = DB_ESQUEMA + "INC_GetDashboardSINMIPorFiltro";

        public DashboardSINMIRepository(IConnectionSQL context)
            : base(context)
        {
        }

        public Task<IEnumerable<BE_DashboardSINMI>> GetAll(FE_DashboardSINMIPorFiltro entidad)
        {
            return Task.Run(() => FindByCondition(entidad, SP_GET_ALL));
        }
    }
}
