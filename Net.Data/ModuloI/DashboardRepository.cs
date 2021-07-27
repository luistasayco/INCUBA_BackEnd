using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class DashboardRepository : RepositoryBase<BE_Dashboard>, IDashboardRepository

    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_ALL = DB_ESQUEMA + "INC_GetDashboardAll";

        public DashboardRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_Dashboard>> GetAll(FE_DashboardPorCategoria entidad)
        {
            return Task.Run(() => FindByCondition(entidad, SP_GET_ALL));
        }
    }
}
