using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoFindTxSIMConsolidado: EntityBase
    {
        public string CodigoEmpresa { get; set; }
        public DateTime FecRegistroInicio { get; set; }
        public DateTime FecRegistroFin { get; set; }

        public FE_TxSIMConsolidado RetornaTxSIMConsolidado()
        {
            return new FE_TxSIMConsolidado
            {
                CodigoEmpresa = this.CodigoEmpresa,
                FecRegistroInicio = this.FecRegistroInicio,
                FecRegistroFin = this.FecRegistroFin,
                RegUsuario = this.RegUsuario
            };
        }
    }
}
