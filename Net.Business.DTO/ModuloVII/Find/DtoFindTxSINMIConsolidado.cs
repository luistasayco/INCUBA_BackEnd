using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoFindTxSINMIConsolidado : EntityBase
    {
        public string CodigoEmpresa { get; set; }
        public DateTime FecRegistroInicio { get; set; }
        public DateTime FecRegistroFin { get; set; }

        public FE_TxSINMIConsolidado RetornaTxSINMIConsolidado()
        {
            return new FE_TxSINMIConsolidado
            {
                CodigoEmpresa = this.CodigoEmpresa,
                FecRegistroInicio = this.FecRegistroInicio,
                FecRegistroFin = this.FecRegistroFin,
                RegUsuario = this.RegUsuario
            };
        }
    }
}
