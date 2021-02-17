using Net.Connection.Attributes;
using System;
using System.Collections.Generic;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxRegistroEquipo : EntityBase
    {
        /// <summary>
        /// Id
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdRegistroEquipo { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime? FecRegistro { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime? FecHoraRegistro { get; set; }
        /// <summary>
        /// CodigoEmpresa
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoEmpresa { get; set; }
        /// <summary>
        /// DescripcionEmpresa
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionEmpresa { get; set; }
        /// <summary>
        /// CodigoPlanta
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoPlanta { get; set; }
        /// <summary>
        /// DescripcionPlanta
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionPlanta { get; set; }
        /// <summary>
        /// IdModelo
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoModelo { get; set; }
        /// <summary>
        /// DescripcionModelo
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionModelo { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [DBParameter(SqlDbType.Text, 0, ActionType.Everything)]
        public string FirmaIncuba { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [DBParameter(SqlDbType.Text, 0, ActionType.Everything)]
        public string FirmaPlanta { get; set; }
        /// <summary>
        /// IdUsuarioCierre
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdUsuarioCierre { get; set; }
        /// <summary>
        /// FecCierre
        /// </summary>
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime? FecCierre { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [DBParameter(SqlDbType.Bit, 0, ActionType.Everything)]
        public Boolean? FlgCerrado { get; set; }
        /// <summary>
        /// UsuarioCierre
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string UsuarioCierre { get; set; }
        /// <summary>
        /// ResponsableIncuba
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 150, ActionType.Everything)]
        public string ResponsableIncuba { get; set; }
        /// <summary>
        /// ResponsablePlanta
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 150, ActionType.Everything)]
        public string ResponsablePlanta { get; set; }
        /// <summary>
        /// Fecha inicio de registro - filtro
        /// </summary>
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime? FecRegistroInicio { get; set; }
        /// <summary>
        /// Fecha fin de registro - filtro
        /// </summary>
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime? FecRegistroFin { get; set; }
        /// <summary>
        /// UsuarioCierre
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string UsuarioCreacion { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionTipoExplotacion { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdSubTipoExplotacion { get; set; }
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionSubTipoExplotacion { get; set; }
        [DBParameter(SqlDbType.VarChar, 100, ActionType.Everything)]
        public string EmailFrom { get; set; }
        [DBParameter(SqlDbType.VarChar, 500, ActionType.Everything)]
        public string EmailTo { get; set; }
        [DBParameter(SqlDbType.VarChar, 300, ActionType.Everything)]
        public string NombreArchivo { get; set; }
        [DBParameter(SqlDbType.VarChar, 100, ActionType.Everything)]
        public string JefePlanta { get; set; }
        [DBParameter(SqlDbType.VarChar, 200, ActionType.Everything)]
        public string ObservacionesInvetsa { get; set; }
        [DBParameter(SqlDbType.VarChar, 200, ActionType.Everything)]
        public string ObservacionesPlanta { get; set; }
        public List<BE_TxRegistroEquipoDetalle1> TxRegistroEquipoDetalle1 { get; set; }
        public List<BE_TxRegistroEquipoDetalle2> TxRegistroEquipoDetalle2 { get; set; }
        public List<BE_TxRegistroEquipoDetalle2> TxRegistroEquipoDetalle2NoPredeterminado { get; set; }
        public List<BE_TxRegistroEquipoDetalle3> TxRegistroEquipoDetalle3 { get; set; }
        public List<BE_TxRegistroEquipoDetalle4> TxRegistroEquipoDetalle4 { get; set; }
        public List<BE_TxRegistroEquipoDetalle5> TxRegistroEquipoDetalle5 { get; set; }
        public List<BE_TxRegistroEquipoDetalle6> TxRegistroEquipoDetalle6 { get; set; }
        public List<BE_TxRegistroEquipoDetalle6> TxRegistroEquipoDetalle6Repuestos { get; set; }
        public List<BE_TxRegistroEquipoDetalle7> TxRegistroEquipoDetalle7 { get; set; }
    }
}
