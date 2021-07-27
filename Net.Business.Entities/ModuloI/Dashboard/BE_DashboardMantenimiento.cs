using System;
using Net.Connection.Attributes;
using System.Collections.Generic;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_DashboardMantenimiento
    {
        /// <summary>
        /// Cantidad
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int Cantidad { get; set; }
        /// <summary>
        /// Periodo
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 20, ActionType.Everything)]
        public string Periodo { get; set; }
        /// <summary>
        /// Descripción
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Descripcion { get; set; }
        /// <summary>
        /// Descripcion de Deshboard 
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionDashboard { get; set; }
        /// <summary>
        /// Etiqueta
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Etiqueta { get; set; }

    }

    public class FE_DashboardMantenimientoPorFiltro
    {
        /// <summary>
        /// Fecha de inicio
        /// </summary>
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha de fin
        /// </summary>
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime FechaFin { get; set; }
        /// <summary>
        /// Dashboard
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdDashboard { get; set; }
        /// <summary>
        /// Codigo de Tecnico
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int Tecnico { get; set; }
        /// <summary>
        /// Codigo de Empresa
        /// </summary>
        [DBParameter(SqlDbType.NVarChar,50,ActionType.Everything)]
        public string Empresa { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Modelo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Planta { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Equipo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdUsuario { get; set; }

    }
}
