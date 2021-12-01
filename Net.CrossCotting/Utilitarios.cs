using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Net.CrossCotting
{
    public static class Utilitarios
    {
        public static string GenerarCodigo()
        {
            Random obj = new Random();
            string sCadena = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int longitud = sCadena.Length;
            char cletra;
            int nlongitud = 30;
            string sNuevacadena = string.Empty;

            for (int i = 0; i < nlongitud; i++)
            {
                cletra = sCadena[obj.Next(nlongitud)];
                sNuevacadena += cletra.ToString();
            }
            return sNuevacadena;

        }

        public static async Task<string> SaveFile(string nombreAleatorio, byte[] bytes)
        {
            /*
                Desarrollo
                    \\\\SERVIDOR95\\Users\\InvetsaNet\\Documents\\Auditoria\\INCUBA-FrontEnd\\src\\assets\\file-pdf\\
                Produccion
                    \\\\SERVIDOR96\\Users\\adminauditoria\\Documents\\Auditoria\\Invetsa\\assets\\file-pdf\\
             */

            string bandera = "\\\\SERVIDOR96\\Users\\adminauditoria\\Documents\\Auditoria\\fileExtranet\\" + nombreAleatorio + ".pdf";

            using (FileStream
            fileStream = new FileStream(bandera, FileMode.Create))
            {
                // Write the data to the file, byte by byte.
                for (int i = 0; i < bytes.Length; i++)
                {
                    fileStream.WriteByte(bytes[i]);
                }

                // Set the stream position to the beginning of the file.
                fileStream.Seek(0, SeekOrigin.Begin);
            }

            return bandera;
        }
    }
}
