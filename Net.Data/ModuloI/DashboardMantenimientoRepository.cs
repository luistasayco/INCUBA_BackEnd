using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class DashboardMantenimientoRepository : RepositoryBase<BE_DashboardMantenimiento>, IDashboardMantenimientoRepository
        
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_ALL = DB_ESQUEMA + "INC_GetDashboardMantenimientoPorFiltro";

        public DashboardMantenimientoRepository(IConnectionSQL context)
            : base(context) 
        {
        }
        public Task<IEnumerable<BE_DashboardMantenimiento>> GetAll(BE_Xml entidad) 
        {
            return Task.Run(() => FindByCondition(entidad, SP_GET_ALL));
        }
    }
}
