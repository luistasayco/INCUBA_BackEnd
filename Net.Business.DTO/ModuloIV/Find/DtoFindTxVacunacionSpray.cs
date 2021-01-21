using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoFindTxVacunacionSpray: EntityBase
    {
        public int IdVacunacionSpray { get; set; }
        public string CodigoEmpresa { get; set; }
        public DateTime FecRegistroInicio { get; set; }
        public DateTime FecRegistroFin { get; set; }

        public FE_TxVacunacionSpray RetornaTxVacunacionSpray()
        {
            return new FE_TxVacunacionSpray
            {
                IdVacunacionSpray = this.IdVacunacionSpray,
                CodigoEmpresa = this.CodigoEmpresa,
                FecRegistroInicio = this.FecRegistroInicio,
                FecRegistroFin = this.FecRegistroFin,
                RegUsuario = this.RegUsuario
            };
        }
    }
}
