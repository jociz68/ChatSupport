using System;

namespace Helper
{
    public static class PasswordHash
    {
        public static String GenerateSHA256Hash(String input)
        {
            var salt = "FastObjectFactory";
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        
    }


}
