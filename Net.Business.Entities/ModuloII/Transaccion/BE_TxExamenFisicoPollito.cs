using Net.Connection.Attributes;
using System;
using System.Collections.Generic;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxExamenFisicoPollito: EntityBase
    {
        /// <summary>
        /// IdExamenFisico
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdExamenFisico { get; set; }
        /// <summary>
        /// CodigoEmpresa
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoEmpresa { get; set; }
        /// <summary>
        /// CodigoEmpresa
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
        /// FecRegistro
        /// </summary>
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime? FecRegistro { get; set; }
        /// <summary>
        /// FecHoraRegistro
        /// </summary>
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime? FecHoraRegistro { get; set; }
        /// <summary>
        /// Numero de nacedora
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string ResponsableInvetsa { get; set; }
        /// <summary>
        /// Numero de nacedora
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string ResponsablePlanta { get; set; }
        /// <summary>
        /// Numero de nacedora
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? NumeroNacedora { get; set; }
        /// <summary>
        /// Lote
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string Lote { get; set; }
        /// <summary>
        /// Peso Promedio
        /// </summary>
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal? PesoPromedio { get; set; }
        /// <summary>
        /// Edad Reproductora
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string EdadReproductora { get; set; }
        /// <summary>
        /// Sexo
        /// </summary>
        [DBParameter(SqlDbType.Char, 1, ActionType.Everything)]
        public string Sexo { get; set; }
        /// <summary>
        /// Linea Genetica
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string LineaGenetica { get; set; }
        /// <summary>
        /// Calificacion
        /// </summary>
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal? Calificacion { get; set; }
        /// <summary>
        /// Id Calidad
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdCalidad { get; set; }
        /// <summary>
        /// DescripcionCalidad
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string DescripcionCalidad { get; set; }
        /// <summary>
        /// ColorCalidad
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string ColorCalidad { get; set; }
        /// <summary> 
        /// Uniformidad
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? Uniformidad { get; set; }
        /// <summary>
        /// FirmaInvetsa
        /// </summary>
        [DBParameter(SqlDbType.Text, 0, ActionType.Everything)]
        public string FirmaInvetsa { get; set; }
        /// <summary>
        /// FirmaPlanta
        /// </summary>
        [DBParameter(SqlDbType.Text, 0, ActionType.Everything)]
        public string FirmaPlanta { get; set; }
        //Filtros
        /// <summary>
        /// FecRegistroInicio
        /// </summary>
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime? FecRegistroInicio { get; set; }
        /// <summary>
        /// FecRegistroFin
        /// </summary>
        [DBParameter(SqlDbType.DateTime, 0, ActionType.Everything)]
        public DateTime? FecRegistroFin { get; set; }
        /// <summary>
        /// UsuarioCreacion
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 20, ActionType.Everything)]
        public string UsuarioCreacion { get; set; }
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

        public IEnumerable<BE_TxExamenFisicoPollitoDetalleNew> ListDetalleNew { get; set; }

        public IEnumerable<BE_TxExamenFisicoPollitoDetalleFotos> ListDetalleFotos { get; set; }

        public IEnumerable<BE_TxExamenFisicoPollitoResumen> ListDetalleResumen { get; set; }
    }
}
