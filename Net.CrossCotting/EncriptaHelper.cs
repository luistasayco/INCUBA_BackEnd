using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Net.CrossCotting
{
    //Advanced Encryption Standard(AES), 
    //también conocido como Rijndael(pronunciado “Rain Doll” en inglés), 
    //es un esquema de cifrado por bloques adoptado como un estándar de 
    //cifrado por el gobierno de los Estados Unidos.
    //El AES fue anunciado por el Instituto 
    //Nacional de Estándares y Tecnología (NIST) como FIPS PUB 197 de los 
    //Estados Unidos (FIPS 197) el 26 de noviembre de 2001 después de un 
    //proceso de estandarización que duró 5 años.Se transformó en un estándar 
    //efectivo el 26 de mayo de 2002. Desde 2006, el AES es uno de los algoritmos
    //más populares usados en criptografía simétrica.
    public static class EncriptaHelper
    {
        private static Rfc2898DeriveBytes GetSecretKey()
        {
            const string encryptionKey = "r3turn-d3-$up3rm4n-!!";
            byte[] salt = Encoding.UTF8.GetBytes(encryptionKey);

            var secretKey = new Rfc2898DeriveBytes(encryptionKey, salt);
            return secretKey;
        }


        /// <summary>
        /// Encripta cualquier texto usando el algoridmo Rijndael.
        /// </summary>
        /// <param name="rawText">texto a encryptar</param>
        /// <returns>Array de bytes con el texto encriptado</returns>
        public static byte[] EncryptToByte(string rawText)
        {

            var rijndaelCipher = new RijndaelManaged();
            byte[] rawTextData = Encoding.UTF8.GetBytes(rawText);

            Rfc2898DeriveBytes secretKey = GetSecretKey();


            using (var encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(rawTextData, 0, rawTextData.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// Desencripta un texto previamente encriptado con el algoritmo Rijndael.
        /// </summary>
        /// <param name="encryptByte">Array de bytes del texto encriptado a desencriptar.</param>
        /// <returns>Texto desencriptado.</returns>
        public static string Decrypt(byte[] encryptByte)
        {
            try
            {
                var rijndaelCipher = new RijndaelManaged();

                Rfc2898DeriveBytes secretKey = GetSecretKey();

                using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
                {
                    using (var memoryStream = new MemoryStream(encryptByte))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            var plainText = new byte[encryptByte.Length];
                            int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                            return Encoding.UTF8.GetString(plainText, 0, decryptedCount);
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
