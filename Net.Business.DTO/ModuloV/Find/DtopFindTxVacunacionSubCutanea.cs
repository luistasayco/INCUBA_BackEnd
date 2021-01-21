using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtopFindTxVacunacionSubCutanea: EntityBase
    {
        public int IdVacunacionSubCutanea { get; set; }
        public string CodigoEmpresa { get; set; }
        public DateTime FecRegistroInicio { get; set; }
        public DateTime FecRegistroFin { get; set; }

        public FE_TxVacunacionSubCutanea RetornaTxVacunacionSubCutanea()
        {
            return new FE_TxVacunacionSubCutanea
            {
                IdVacunacionSubCutanea = this.IdVacunacionSubCutanea,
                CodigoEmpresa = this.CodigoEmpresa,
                FecRegistroInicio = this.FecRegistroInicio,
                FecRegistroFin = this.FecRegistroFin,
                RegUsuario = this.RegUsuario
            };
        }
    }
}
