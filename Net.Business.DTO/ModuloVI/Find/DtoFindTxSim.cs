using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoFindTxSim: EntityBase
    {
        public int IdSIM { get; set; }
        public string CodigoEmpresa { get; set; }
        public DateTime FecRegistroInicio { get; set; }
        public DateTime FecRegistroFin { get; set; }

        public FE_TxSIM RetornaTxSIM()
        {
            return new FE_TxSIM
            {
                IdSIM = this.IdSIM,
                CodigoEmpresa = this.CodigoEmpresa,
                FecRegistroInicio = this.FecRegistroInicio,
                FecRegistroFin = this.FecRegistroFin,
                RegUsuario = this.RegUsuario
            };
        }
    }
}
