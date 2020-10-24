using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindModelo
    {
        public string CodigoModelo { get; set; }
        public string Descripcion { get; set; }
        public BE_Modelo Modelo()
        {
            return new BE_Modelo
            {
                CodigoModelo = this.CodigoModelo,
                Descripcion = this.Descripcion
            };
        }
    }
}
