using System.Collections.Generic;
using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateTxVacunacionSpray: EntityBase
    {
        public int IdVacunacionSpray { get; set; }
        public IEnumerable<BE_TxVacunacionSprayFotos> ListarTxVacunacionSprayFotos { get; set; }

        public BE_TxVacunacionSpray RetornaTxVacunacionSpray()
        {
            return new BE_TxVacunacionSpray
            {
                IdVacunacionSpray = this.IdVacunacionSpray,
                ListarTxVacunacionSprayFotos = this.ListarTxVacunacionSprayFotos,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
