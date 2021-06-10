using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _5_practical_task
{

    class RSACSPSample
    {
        public ASCIIEncoding ByteConverter = new ASCIIEncoding();
        public RSAParameters Key;
        public string test;

        public byte[] HashAndSignBytes(string DataToSign)
        {
            try
            {
                RSACryptoServiceProvider RSAalgoritmas = new RSACryptoServiceProvider();

                Key = RSAalgoritmas.ExportParameters(true);
                byte[] originalData = ByteConverter.GetBytes(DataToSign);
                // Sukuriamas naujas „RSACryptoServiceProvider“ egzempliorius naudodami iš „RSAParameters“.

                RSAalgoritmas.ImportParameters(Key);
                 test = RSAalgoritmas.ToXmlString(true);
                // Hash'inam ir pasirašom duomenis. Perduodam naują SHA256 egzempliorių, kad nurodytumėte maišos algoritmą.
                return RSAalgoritmas.SignData(originalData, SHA256.Create());

            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public static bool VerifySignedHash(byte[] DataToVerify, byte[] SignedData, string Key)
        {
            try
            {
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

                RSAalg.FromXmlString(Key);

                return RSAalg.VerifyData(DataToVerify, SHA256.Create(), SignedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }
    }
}
