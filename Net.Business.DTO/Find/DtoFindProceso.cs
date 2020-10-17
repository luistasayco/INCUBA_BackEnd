using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindProceso
    {
        public int IdProceso { get; set; }
        public string Descripcion { get; set; }

        public BE_Proceso Proceso()
        {
            return new BE_Proceso
            {
                IdProceso = this.IdProceso,
                Descripcion = this.Descripcion
            };
        }
    }
}
