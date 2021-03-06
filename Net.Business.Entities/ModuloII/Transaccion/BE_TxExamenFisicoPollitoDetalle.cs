﻿using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxExamenFisicoPollitoDetalle: EntityBase
    {
        /// <summary>
        /// IdExamenFisicoDetalle
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdExamenFisicoDetalle { get; set; }
        /// <summary>
        /// IdExamenFisico
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdExamenFisico { get; set; }
        /// <summary>
        /// NumeroPollito
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int NumeroPollito { get; set; }
        /// <summary>
        /// DescripcionProceso
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionProceso { get; set; }
        /// <summary>
        /// IdProcesoDetalle
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdProcesoDetalle { get; set; }
        /// <summary>
        /// descripcionProcesoDetalle
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionProcesoDetalle { get; set; }
        /// <summary>
        /// Valor
        /// </summary>
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal Valor { get; set; }
        /// <summary>
        /// Factor
        /// </summary>
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal Factor { get; set; }
    }
}
