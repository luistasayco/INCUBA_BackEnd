using System.Data;
using Net.Connection.Attributes;

namespace Net.Business.Entities
{
    public class BE_DashboardFormulario
    {
        /// <summary>
        /// 
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string ResponsableIncubadora { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string NombreVacunador { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Descripcion { get; set; }
    }

    public class FE_DashboardFormularioPorFiltro
    {
        /// <summary>
        /// 1 => ResponsableIncubadora
        /// 2 => NombreVacunador
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int Filtro { get; set; }

    }
}
