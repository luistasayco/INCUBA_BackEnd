using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteProceso : EntityBase
    {
        public int IdProceso { get; set; }

        public BE_Proceso Proceso()
        {
            return new BE_Proceso
            {
                IdProceso = this.IdProceso,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
