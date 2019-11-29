using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using NLog;
using Server.DAL;

namespace Server.Function
{
    class UserManager
    {
        private const int Pbkdf2Iterations = 100000;
        public const int HashSize = 32;
        public const int SaltSize = 128;
        public const int TokenSize = 64;

        private static Logger _log = LogManager.GetCurrentClassLogger();

        #region Login
        public byte[] Login(string username, string password)
        {
            var db = new DatabaseEntities();
            var account = db.accounts.Find(username);

            // Check if account was found
            if (account != null &&
                VerifyPassword(password, account.PasswordSalt, account.PasswordHash))
            {
                var token = GenerateLoginToken();

                db.tokens.Add(new token()
                {
                    account = account,
                    AccessToken = token
                });
                db.SaveChanges();
                return token;
            }

            // On error return empty array
            return new byte[0];
        }

        private byte[] GenerateLoginToken()
        {
            // Generate a random token for the user to login with
            var rng = new RNGCryptoServiceProvider();
            byte[] token = new byte[TokenSize];
            rng.GetBytes(token);
            return token;
        }
        #endregion

        #region Create
        public bool Create(string username, string password)
        {
            var db = new DatabaseEntities();

            // Check if username is already taken
            if (db.accounts.Find(username) != null)
                return false;
            
            // Generate a hash and salt for the new user
            var pw_info = GenerateHashedPasswordAndSalt(password);
            
            var account = new account()
            {
                Username = username,
                PasswordHash = pw_info.password,
                PasswordSalt = pw_info.salt
            };
            db.accounts.Add(account);
            db.SaveChanges();

            return true;
        }
        #endregion

        public member GetMemberFromToken(byte[] token)
        {
            var db = new DatabaseEntities();
            var tokenLocation = (from t in db.tokens where t.AccessToken == token select t).FirstOrDefault();

            var r = tokenLocation?.account.members.SingleOrDefault(m => m.Username == tokenLocation.AccountUsername);
            Debug.WriteLine($"{r}, {BitConverter.ToString(token)}");
            return r;
        }

        private (byte[] password, byte[] salt) GenerateHashedPasswordAndSalt(string password)
        {
            // Generate a new salt
            var rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            // Hash password with salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Pbkdf2Iterations);
            byte[] passwordHash = pbkdf2.GetBytes(HashSize);

            return (passwordHash, salt);
        }

        private bool VerifyPassword(string inputPw, byte[] salt, byte[] hashedPw)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(inputPw, salt, Pbkdf2Iterations);
            byte[] inputPwHash = pbkdf2.GetBytes(HashSize);

            return inputPwHash.SequenceEqual(hashedPw);
        }
    }
}
