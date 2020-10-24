using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxRegistroEquipoDetalle2 : EntityBase
    {
        /// <summary>
        /// IdRegistroEquipoDetalle
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdRegistroEquipoDetalle { get; set; }
        /// <summary>
        /// IdRegistroEquipo
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdRegistroEquipo { get; set; }
        /// <summary>
        /// IdRepuestoPorModelo
        /// </summary>
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdRepuestoPorModelo { get; set; }
        /// <summary>
        /// CodigoRepuesto
        /// </summary>
        [DBParameter(SqlDbType.NVarChar , 50, ActionType.Everything)]
        public string CodigoRepuesto { get; set; }
        /// <summary>
        /// Descripcion
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 100, ActionType.Everything)]
        public string Descripcion { get; set; }
        /// <summary>
        /// CodigoEquipo
        /// </summary>
        [DBParameter(SqlDbType.NVarChar, 50, ActionType.Everything)]
        public string CodigoEquipo { get; set; }
        /// <summary>
        /// MP
        /// </summary>
        [DBParameter(SqlDbType.Char, 1, ActionType.Everything)]
        public string Mp { get; set; }
        /// <summary>
        /// FlgValor
        /// </summary>
        [DBParameter(SqlDbType.Bit, 0, ActionType.Everything)]
        public bool FlgValor { get; set; }
        /// <summary>
        /// RFC
        /// </summary>
        [DBParameter(SqlDbType.Char, 1, ActionType.Everything)]
        public string Rfc { get; set; }
    }
}
