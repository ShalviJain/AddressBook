using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AddressBook
{
    public static class Cyrpto
    {
        public static string Hash(string value)
        {
           return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value)));
            
            //var sha1 = System.Security.Cryptography.SHA256.Create();
            //var inputBytes = Encoding.ASCII.GetBytes(value);
            //var hash = sha1.ComputeHash(inputBytes);

            //var sb = new StringBuilder();
            //for (var i = 0; i < hash.Length; i++)
            //{
            //    sb.Append(hash[i].ToString("X2"));
            //}
            //return sb.ToString();
        }

        public static string RandomString()
        {
            int length = 6;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}