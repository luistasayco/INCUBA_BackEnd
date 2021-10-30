using System.Collections.Generic;
using Net.Connection;
using Net.Business.Entities;
using System.Threading.Tasks;

namespace Net.Data
{
    public class DashboardAuditoriaRepository : RepositoryBase<BE_DashboardAuditoria>, IDashboardAuditoriaRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_ALL = DB_ESQUEMA + "INC_GetDashboardAuditoriaPorFiltro";

        public DashboardAuditoriaRepository(IConnectionSQL context)
            : base(context)
        {
        }
        
        public Task<IEnumerable<BE_DashboardAuditoria>> GetAll(BE_Xml entidad)
        {
            return Task.Run(()=> FindByCondition(entidad, SP_GET_ALL));
        }
    }
}
