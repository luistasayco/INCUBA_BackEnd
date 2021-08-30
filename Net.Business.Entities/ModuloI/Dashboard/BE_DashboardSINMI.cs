using System;
using System.Collections.Generic;
using System.Text;
using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_DashboardSINMI
    {   
        [DBParameter(SqlDbType.Decimal,0,ActionType.Everything)]
        public decimal ScoreGeneral { get; set; }
        [DBParameter(SqlDbType.NChar, 6, ActionType.Everything)]
        public string Periodo { get; set; }
    }

    public class FE_DashboardSINMIPorFiltro 
    {
        /// <summary>
        /// Empresa
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Empresa { get; set; }
        /// <summary>
        /// Planta
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Planta { get; set; }
        /// <summary>
        /// Responsable Invetsa
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int ResponsableInvetsa { get; set; }
        /// <summary>
        /// Responsable Incubadora
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string ResponsableCompañia { get; set; }
        /// <summary>
        /// Tipo 
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int TipoModulo { get; set; }
        /// <summary>
        /// Linea Genetica
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Linea{ get; set; }
        /// <summary>
        /// Edad
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int Edad { get; set; }
        /// <summary>
        /// Fecha Inicio
        /// </summary>
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha Fin
        /// </summary>
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime FechaFin { get; set; }
        /// <summary>
        /// Id Dashboard
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdDashboard { get; set; }
        /// <summary>
        /// IdUsuario
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdUsuario { get; set; }
    }
}
