using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoDeleteIndiceEficiencia: EntityBase
    {
        public int IdIndiceEficiencia { get; set; }

        public BE_IndiceEficiencia RetornarIndiceEficiencia()
        {
            return new BE_IndiceEficiencia
            {
                IdIndiceEficiencia = this.IdIndiceEficiencia,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
