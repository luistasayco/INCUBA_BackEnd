using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoFindTxSINMI : EntityBase
    {
        public int IdSINMI { get; set; }
        public string CodigoEmpresa { get; set; }
        public DateTime FecRegistroInicio { get; set; }
        public DateTime FecRegistroFin { get; set; }

        public FE_TxSINMI RetornaTxSINMI()
        {
            return new FE_TxSINMI
            {
                IdSINMI = this.IdSINMI,
                CodigoEmpresa = this.CodigoEmpresa,
                FecRegistroInicio = this.FecRegistroInicio,
                FecRegistroFin = this.FecRegistroFin,
                RegUsuario = this.RegUsuario
            };
        }
    }
}
