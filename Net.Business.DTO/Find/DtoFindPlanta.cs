using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindPlanta
    {
        public string CodigoEmpresa { get; set; }
        public string Descripcion { get; set; }
        public BE_Planta Planta()
        {
            return new BE_Planta
            {
                CodigoEmpresa = this.CodigoEmpresa,
                Descripcion = this.Descripcion
            };
        }
    }
}
