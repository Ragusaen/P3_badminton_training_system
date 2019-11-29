﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using Common.Model;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.LayoutRenderers;
using Remotion.Linq.Clauses;
using Server.DAL;

namespace Server.Controller
{
    class UserManager
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        // Defines how thorough the hashing function should be
        private const int Pbkdf2Iterations = 100000; 

        // Sizes of data
        public const int HashSize = 32;
        public const int SaltSize = 128;
        public const int TokenSize = 64;

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
                    AccessToken = token
                });
                db.SaveChanges();
                return token;
            }

            // On error return empty array
            return new byte[0];
        }

        /// <summary>
        /// Generates cryptographically random bytes to be used as a login token
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
            var tokenLocation = (from t in db.tokens where t.AccessToken == token select t).FirstOrDefault();

            var r = tokenLocation?.account.members.SingleOrDefault(m => m.Username == tokenLocation.AccountUsername);
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
        /// <param name="inputPw">The raw password to test against</param>
        /// <param name="salt"> The salt to append to the raw password</param>
        /// <param name="hashedPw"> The stored hashed password</param>
        /// <returns>true, if password is the same as the one originally used for the stored hashed password; otherwise, false</returns>
        private bool VerifyPassword(string inputPw, byte[] salt, byte[] hashedPw)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(inputPw, salt, Pbkdf2Iterations);
            byte[] inputPwHash = pbkdf2.GetBytes(HashSize);

            return inputPwHash.SequenceEqual(hashedPw);
        }
    }
}
