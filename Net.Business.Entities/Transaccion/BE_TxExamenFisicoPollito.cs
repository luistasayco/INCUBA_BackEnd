using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxExamenFisicoPollito: EntityBase
    {
        /// <summary>
        /// Id
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int Id { get; set; }
        /// <summary>
        /// Numero de Hoja
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int NumeroHoja { get; set; }
        /// <summary>
        /// Numero de nacedora
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int NumeroNacedora { get; set; }
        /// <summary>
        /// Lote
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public int Lote { get; set; }
        /// <summary>
        /// Peso Promedio
        /// </summary>
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal PesoPromedio { get; set; }
        /// <summary>
        /// Edad Reproductora
        /// </summary>
        [DBParameter(SqlDbType.Decimal, 0, ActionType.Everything)]
        public decimal EdadReproductora { get; set; }
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
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int Calificacion { get; set; }
        /// <summary>
        /// Id Calidad
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdCalidad { get; set; }
    }
}
