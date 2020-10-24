using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindEmpresa
    {
        public string CodigoEmpresa { get; set; }
        public string Descripcion { get; set; }

        public BE_Empresa Empresa()
        {
            return new BE_Empresa
            {
                CodigoEmpresa = this.CodigoEmpresa,
                Descripcion = this.Descripcion
            };
        }
    }
}
