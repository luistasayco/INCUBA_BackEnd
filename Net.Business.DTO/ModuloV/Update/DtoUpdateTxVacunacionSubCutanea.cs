using System.Collections.Generic;
using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateTxVacunacionSubCutanea: EntityBase
    {
        public int IdVacunacionSubCutanea { get; set; }
        public IEnumerable<BE_TxVacunacionSubCutaneaFotos> ListarTxVacunacionSubCutaneaFotos { get; set; }

        public BE_TxVacunacionSubCutanea RetornaTxVacunacionSubCutanea()
        {
            return new BE_TxVacunacionSubCutanea
            {
                IdVacunacionSubCutanea = this.IdVacunacionSubCutanea,
                ListarTxVacunacionSubCutaneaFotos = this.ListarTxVacunacionSubCutaneaFotos,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
