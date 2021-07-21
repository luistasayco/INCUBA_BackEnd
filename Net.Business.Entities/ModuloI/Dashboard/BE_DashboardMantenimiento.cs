using System;
using Net.Connection.Attributes;
using System.Collections.Generic;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_DashboardMantenimiento
    {
        /// <summary>
        /// Cantidad de Visitias realizadas por el tecnico
        /// </summary>
        [DBParameter(SqlDbType.Int,0, ActionType.Everything)]
        public int CantidadVisitas { get; set; }
        /// <summary>
        /// Año de visita
        /// </summary>
        [DBParameter(SqlDbType.NVarChar,20,ActionType.Everything)]
        public string Periodo { get; set; }
        /// <summary>
        /// Codidgo de empresa
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string NombreEmpresa { get; set; }
        /// <summary>
        /// Codigo de planta
        /// </summary>
        [DBParameter(SqlDbType.NVarChar,50, ActionType.Everything)]
        public string NombrePlanta { get; set; }
        /// <summary>
        /// Nombre del Modelo
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string NombreModelo { get; set; }
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string NombreRepuesto { get; set; }

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
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Modelo { get; set; }
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Equipo { get; set; }

    }
}
