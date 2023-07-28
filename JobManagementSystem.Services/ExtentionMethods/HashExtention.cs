using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace JobManagementSystem.Services.ExtentionMethods
{
    public static class HashExtention
    {
        public static string HashPassword(this string password)
        {
            using (var sha256 = new SHA256Managed())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);

                // Convert the hashed bytes to a hexadecimal string
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in hash)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
