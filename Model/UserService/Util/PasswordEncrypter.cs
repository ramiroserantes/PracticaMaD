using System;
using System.Text;
using System.Security.Cryptography;


namespace Es.Udc.DotNet.PracticaMaD.Model.UserService.Util
{
    public static class PasswordEncrypter
    {                      
        public static String Crypt(String password)
        {
            HashAlgorithm hashAlg = new SHA256Managed();

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            byte[] encryptedPasswordBytes = hashAlg.ComputeHash(passwordBytes);

            String encryptedPassword = Convert.ToBase64String(encryptedPasswordBytes);

            return encryptedPassword;
        }

        public static Boolean IsClearPasswordCorrect(String clearPassword,
            String encryptedPassword)
        {
            string encryptedClearPassword = Crypt(clearPassword);

            return encryptedClearPassword.Equals(encryptedPassword);
        }
    }
}
