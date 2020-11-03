using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoFindTxExamenFisicoPollitoPorFiltro
    {
        public int IdExamenFisico { get; set; }
        public string CodigoEmpresa { get; set; }
        public DateTime FecRegistroInicio { get; set; }
        public DateTime FecRegistroFin { get; set; }

        public FE_TxExamenFisicoPollito RetornaTxExamenFisicoPollito()
        {
            return new FE_TxExamenFisicoPollito
            {
                IdExamenFisico = this.IdExamenFisico,
                CodigoEmpresa = this.CodigoEmpresa,
                FecRegistroInicio = this.FecRegistroInicio,
                FecRegistroFin = this.FecRegistroFin
            };
        }
    }
}
