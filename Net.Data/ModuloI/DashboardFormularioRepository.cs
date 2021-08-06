using System.Collections.Generic;
using Net.Business.Entities;
using Net.Connection;
using System.Threading.Tasks;

namespace Net.Data
{
    public class DashboardFormularioRepository : RepositoryBase<BE_DashboardFormulario>, IDashboardFormularioRepository
    {
        const string DB_ESQUEMA = "DBO.";
        const string SP_GET_ALL = DB_ESQUEMA + "INC_GetDatosDashboardPorFiltro";

        public DashboardFormularioRepository(IConnectionSQL context)
            : base(context) 
        { }

        public Task<IEnumerable<BE_DashboardFormulario>> GetAll(FE_DashboardFormularioPorFiltro entidad) 
        {
            return Task.Run(() => FindByCondition(entidad, SP_GET_ALL));
        }
    }
}
