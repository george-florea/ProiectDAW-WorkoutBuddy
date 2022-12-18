using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Common.Extensions
{
    public static class HashingExtensions
    {
        public static byte[] HashPassword(this string password, Guid UserSalt)
        {
            var salt = UserSalt.ToString();
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] passwordHash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                
                return passwordHash;
            }
        }
    }
}
