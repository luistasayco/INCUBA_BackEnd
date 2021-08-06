using System;
using System.Collections.Generic;
using System.Text;
using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_DashboardAuditoria 
    {
        /// <summary>
        /// Cantidad
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int Cantidad { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int CantidadSI { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int CantidadNO { get; set; }
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
        /// <summary>
        /// Puntaje productividad
        /// </summary>
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal PuntajeProductividad { get; set; }
        /// <summary>
        /// Porcentaje de eficiencia
        /// </summary>
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal PorcentajeEficiencia { get; set; }
        /// <summary>
        /// Puntaje total obtenido
        /// </summary>
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal ValorObtenido { get; set; }
        /// <summary>
        /// Nombre Vacunador
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string NombreVacunador { get; set; }
        
    }
    public class FE_DashboardAuditoriaPorFiltro
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
        public string ResponsablePlanta { get; set; }
        /// <summary>
        /// Tipo de auditoria
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int Tipo { get; set; }
        /// <summary>
        /// Linea Genetica
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int LineaGenetica { get; set; }
        /// <summary>
        /// Vacunador
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Vacunador { get; set; }
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
