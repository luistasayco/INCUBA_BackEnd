using System;
using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_Dashboard
    {
        /// <summary>
        /// Identificador de dashboard
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int DashboardID { get; set; }
        /// <summary>
        /// Nombre del dashboard
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DashboardName { get; set; }
        /// <summary>
        /// Descripción opcional
        /// </summary>
        [DBParameter(SqlDbType.Int, 0 , ActionType.Everything)]
        public int DashboardCategoryID { get; set; }

    }

    public class FE_DashboardPorCategoria
    {
        /// <summary>
        /// Mantenimiento, Auditoria o SINMI
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DashboardCategory { get; set; }

    }
}
