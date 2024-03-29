﻿using System;
using System.Collections.Generic;
using Net.Connection.Attributes;
using System.Data;

namespace Net.Business.Entities
{
    public class BE_TxSINMIConsolidado : EntityBase
    {
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int IdSINMIConsolidado { get; set; }
        public DateTime? FecHoraRegistro { get; set; }
        public string Descripcion { get; set; }
        public string Conclusion { get; set; }
        public string Resultado { get; set; }
        public string Linea { get; set; }
        public string PersonaContacto { get; set; }
        public string DescripcionEmpresa { get; set; }
        public string UsuarioCreacion { get; set; }
        [DBParameter(SqlDbType.Int, 0, ActionType.Everything)]
        public int? IdUsuarioCierre { get; set; }
        public string UsuarioCierre { get; set; }
        public DateTime? FecCierre { get; set; }
        public Boolean? FlgCerrado { get; set; }
        public string NombreArchivo { get; set; }
        public IEnumerable<BE_TxSINMIConsolidadoDetalle> ListaTxSINMIConsolidadoDetalle { get; set; }
    }
}
