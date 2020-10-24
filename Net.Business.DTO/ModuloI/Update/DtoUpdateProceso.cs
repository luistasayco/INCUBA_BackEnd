using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoUpdateProceso : EntityBase
    {
        public int IdProceso { get; set; }
        public string Descripcion { get; set; }
        public decimal Factor { get; set; }
        public int Orden { get; set; }
        public BE_Proceso Proceso()
        {
            return new BE_Proceso
            {
                IdProceso = this.IdProceso,
                Descripcion = this.Descripcion,
                Factor = this.Factor,
                Orden = this.Orden,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
