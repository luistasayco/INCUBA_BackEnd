using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoInsertRepuestoPorModelo : EntityBase
    {
        public int IdRepuestoPorModelo { get; set; }
        public string CodigoModelo { get; set; }
        public string CodigoRepuesto { get; set; }
        public Boolean FlgPredeterminado { get; set; }
        public Boolean? FlgAccesorio { get; set; }
        public BE_RepuestoPorModelo RepuestoPorModelo()
        {
            return new BE_RepuestoPorModelo
            {
                IdRepuestoPorModelo = this.IdRepuestoPorModelo,
                CodigoModelo = this.CodigoModelo,
                CodigoRepuesto = this.CodigoRepuesto,
                FlgPredeterminado = this.FlgPredeterminado,
                FlgAccesorio = this.FlgAccesorio,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
