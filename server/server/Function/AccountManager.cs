using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using NLog;
using Server.DAL;

namespace Server.Function
{
    class AccountManager
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        // Defines how thorough the hashing function should be
        private const int Pbkdf2Iterations = 15000; 

        // Sizes of data
        public const int HashSize = 32;
        public const int SaltSize = 32;
        public const int TokenSize = 64;
        public TimeSpan AccessTokenDuration = TimeSpan.FromHours(1);

        /// <summary>
        /// Logs in the user given a username and password.
        /// </summary>
        /// <returns> a valid access token if user exists and password is correct; otherwise an empty byte array</returns>
        public byte[] Login(string username, string password)
        {
            var db = new DatabaseEntities();

            // Find the account with the given username
            var account = db.accounts.Find(username);

            // Check if account was found and if the password matches
            if (account != null &&
                VerifyPassword(password, account.PasswordSalt, account.PasswordHash))
            {
                // Generate and access token
                var token = GenerateLoginToken();

                // Add the new token to database
                db.tokens.Add(new token()
                {
                    account = account,
                    AccessToken = token,
                    ValidUntil = DateTime.Now + AccessTokenDuration
                });
                db.SaveChanges();
                return token;
            }

            // On error return empty array
            return new byte[0];
        }

        /// <summary>
        /// Generates cryptographically secure random bytes to be used as a login token
        /// </summary>
        private byte[] GenerateLoginToken()
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] token = new byte[TokenSize];
            rng.GetBytes(token);
            return token;
        }

        /// <summary>
        /// Create a new user with the given username and password
        /// </summary>
        /// <returns>true, if the username is not already taken and the user was created; otherwise, false</returns>
        public bool Create(string username, string password)
        {
            var db = new DatabaseEntities();

            // Check if username is already taken
            if (db.accounts.Find(username) != null)
                return false;
            
            // Generate a hash and salt for the new user
            var pw_info = GenerateHashedPasswordAndSalt(password);
            
            // Create the account in the database
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

        /// <summary>
        /// Gets the database member that is associated with an access token
        /// </summary>
        public member GetMemberFromToken(byte[] token)
        {
            var db = new DatabaseEntities();

            // Remove all invalid tokens
            db.tokens.RemoveRange(db.tokens.Where(t => t.ValidUntil < DateTime.Now));

            // Get the specified token
            var dbToken = (from t in db.tokens where t.AccessToken == token select t).FirstOrDefault();

            // Find the tokens user
            var r = dbToken?.account.members.SingleOrDefault(m => m.Username == dbToken.AccountUsername);
            Debug.WriteLine($"{r}, {BitConverter.ToString(token)}");
            return r;
        }

        /// <summary>
        /// Generates the hashed password and a salt to go with it. This method should be used when setting a new password.
        /// The hashed password is computed by hash( password + salt ), so the salt should be saved for password verification.
        /// </summary>
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

        /// <summary>
        /// Verifies that the given password matches the one that was used to create the password hash.
        /// </summary>
        /// <param name="loginPassword">The raw password to test against</param>
        /// <param name="salt"> The salt to append to the raw password</param>
        /// <param name="hashedPassword"> The stored hashed password</param>
        /// <returns>true, if password is the same as the one originally used for the stored hashed password; otherwise, false</returns>
        private bool VerifyPassword(string loginPassword, byte[] salt, byte[] hashedPassword)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(loginPassword, salt, Pbkdf2Iterations);

            var s = new Stopwatch();
            s.Start();
            byte[] inputPwHash = pbkdf2.GetBytes(HashSize);
            s.Stop();

            Console.WriteLine($"PBKDF2 took {s.ElapsedMilliseconds} ms");

            return inputPwHash.SequenceEqual(hashedPassword);
        }
    }
}
