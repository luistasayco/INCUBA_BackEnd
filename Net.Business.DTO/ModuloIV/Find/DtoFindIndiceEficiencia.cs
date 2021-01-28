using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindIndiceEficiencia
    {
        public int IdIndiceEficiencia { get; set; }
        public string DescripcionIndiceEficiencia { get; set; }

        public BE_IndiceEficiencia RetornarIndiceEficiencia()
        {
            return new BE_IndiceEficiencia
            {
                IdIndiceEficiencia = this.IdIndiceEficiencia,
                DescripcionIndiceEficiencia = this.DescripcionIndiceEficiencia
            };
        }
    }
}
