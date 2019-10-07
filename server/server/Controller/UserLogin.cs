using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace server.Controller
{
    class UserLogin
    {
        public const int pbkdf2_iterations = 100000;
        public const int hash_size = 32;

        struct UserInfo
        {
            public string Username;
            public byte[] PasswordHash;
            public byte[] Salt;
        }

        private UserInfo? FindUser(string username)
        {
            //Actually find the user in the database !!!
            var pbkdf2 = new Rfc2898DeriveBytes(Encoding.ASCII.GetBytes("password123"), Encoding.ASCII.GetBytes("randomstring"), pbkdf2_iterations);
            return new UserInfo
            {
                Username = "hansemand",
                PasswordHash = pbkdf2.GetBytes(hash_size),
                Salt = Encoding.ASCII.GetBytes("randomstring")
            };
        }

        public byte[] Login(string username, byte[] password)
        {
            UserInfo? user_info = FindUser(username); 
            if (user_info.HasValue)
            {
                UserInfo user = user_info.Value;

                var pbkdf2 = new Rfc2898DeriveBytes(password, user.Salt, pbkdf2_iterations);
                byte[] input_pw_hash = pbkdf2.GetBytes(hash_size);

                if (user.PasswordHash.SequenceEqual(input_pw_hash))
                {
                    var rng = new RNGCryptoServiceProvider();
                    byte[] token = new byte[hash_size];
                    rng.GetBytes(token);

                    return token;
                }
            }

            return new byte[0];
        }
    }
}
