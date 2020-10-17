using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindGeneral
    {
        public string CodigoEmpresa { get; set; }
        public string CodigoPlanta { get; set; }
        public string CodigoModelo { get; set; }
        public BE_General General()
        {
            return new BE_General
            {
                CodigoEmpresa = this.CodigoEmpresa,
                CodigoPlanta = this.CodigoPlanta,
                CodigoModelo = this.CodigoModelo
            };
        }
    }
}
