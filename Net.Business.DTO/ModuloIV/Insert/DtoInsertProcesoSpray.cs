using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoInsertProcesoSpray: EntityBase
    {
        public int IdProcesoSpray { get; set; }
        public string DescripcionProcesoSpray { get; set; }
        public decimal Valor { get; set; }

        public BE_ProcesoSpray RetornaProcesoSpray()
        {
            return new BE_ProcesoSpray
            {
                IdProcesoSpray = this.IdProcesoSpray,
                DescripcionProcesoSpray = this.DescripcionProcesoSpray,
                Valor = this.Valor,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
