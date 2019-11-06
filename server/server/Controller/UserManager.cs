﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Server.DAL;
using MySql.Data.MySqlClient;
using System.Data;

namespace Server.Controller
{
    class UserManager
    {
        private const int Pbkdf2Iterations = 100000;
        public const int HashSize = 32;
        public const int SaltSize = 128;
        public const int TokenSize = 64;

        struct UserInfo
        {
            public readonly string Username;
            public readonly byte[] PasswordHash;
            public readonly byte[] Salt;

            public UserInfo(string username, byte[] passwordHash, byte[] salt)
            {
                Username = username;
                PasswordHash = passwordHash;
                Salt = salt;
            }
        }

        #region Login
        public byte[] Login(string username, string password)
        {
            UserInfo? userInfo = FindUser(username);
            if (userInfo.HasValue)
            {
                UserInfo user = userInfo.Value;

                if (VerifyPassword(password, user.Salt, user.PasswordHash))
                {
                    return GenerateLoginToken();
                }
            }

            return new byte[0];
        }

        private UserInfo? FindUser(string username)
        {
            string query = string.Format("select Username, PasswordHash, PasswordSalt from `Account` where Username=@Username;");
            MySqlParameter[] param = new MySqlParameter[1];
            param[0] = new MySqlParameter("@Username", username);

            DBConnection db = new DBConnection();
            DataTable dt = db.ExecuteSelectQuery(query, param);

            if (dt.Rows.Count != 1)
            {
                return null;
            }
            return new UserInfo(dt.Rows[0][0].ToString(), (byte[])dt.Rows[0][1], (byte[])dt.Rows[0][2]);
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
            if (FindUser(username).HasValue)
                return false;
            
            var pw_info = GenerateHashedPasswordAndSalt(password);
            UserInfo userInfo = new UserInfo(
                username,
                pw_info.password,
                pw_info.salt
            );

            AddUserToDatabase(userInfo);

            return true;
        }
        #endregion

        #region Database
        private void AddUserToDatabase(UserInfo userInfo)
        {
            string query = string.Format("insert into `Account`(Username, PasswordHash, PasswordSalt) values(@Username, @Hash, @Salt);");
            MySqlParameter[] param = new MySqlParameter[5];
            param[2] = new MySqlParameter("@Username", userInfo.Username);
            param[3] = new MySqlParameter("@Hash", userInfo.PasswordHash);
            param[4] = new MySqlParameter("@Salt", userInfo.Salt);

            DBConnection db = new DBConnection();
            db.ExecuteInsertUpdateDeleteQuery(query, param);
        }
        #endregion

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
