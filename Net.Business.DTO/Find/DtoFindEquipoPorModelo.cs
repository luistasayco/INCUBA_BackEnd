using Net.Business.Entities;

namespace Net.Business.DTO
{
    public class DtoFindEquipoPorModelo
    {
        public string CodigoEmpresa { get; set; }
        public string CodigoPlanta { get; set; }
        public string CodigoModelo { get; set; }

        public BE_EquipoPorModelo EquipoPorModelo()
        {
            return new BE_EquipoPorModelo
            {
                CodigoEmpresa = this.CodigoEmpresa,
                CodigoPlanta = this.CodigoPlanta,
                CodigoModelo = this.CodigoModelo
            };
        }
    }
}
