﻿using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoUpdateTxRegistroDocumento: EntityBase
    {
        public int IdDocumento { get; set; }
        public string IdGoogleDrive { get; set; }
        public string NombreArchivo { get; set; }

        public BE_TxRegistroDocumento RetornaTxRegistroDocumento()
        {
            return new BE_TxRegistroDocumento
            {
                IdDocumento = this.IdDocumento,
                IdGoogleDrive = this.IdGoogleDrive,
                NombreArchivo = this.NombreArchivo,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
