using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using server.DAL;
using MySql.Data.MySqlClient;
using Server.Model;
using System.Data;

namespace Server.Controller
{
    class User
    {
        private const int pbkdf2_iterations = 100000;
        private const int hash_size = 32;
        private const int salt_size = 128;
        private const int token_size = 64;
        

        struct UserInfo
        {
            public readonly string Username;
            public readonly byte[] PasswordHash;
            public readonly byte[] Salt;

            public UserInfo(string username, byte[] password_hash, byte[] salt)
            {
                Username = username;
                PasswordHash = password_hash;
                Salt = salt;
            }
        }

        #region Login
        public byte[] Login(string username, string password)
        {
            UserInfo? user_info = FindUser(username);
            if (user_info.HasValue)
            {
                UserInfo user = user_info.Value;

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
            return new UserInfo(dt.Rows[0][0].ToString(), (byte[])dt.Rows[0][1], (byte[])dt.Rows[0][2]);
        }

        private byte[] GenerateLoginToken()
        {
            // Generate a random token for the user to login with
            var rng = new RNGCryptoServiceProvider();
            byte[] token = new byte[token_size];
            rng.GetBytes(token);
            return token;
        }
        #endregion

        #region Create
        public void Create(string username, string password, Member member)
        {
            var pw_info = GenerateHashedPasswordAndSalt(password);
            UserInfo user_info = new UserInfo(
                username,
                pw_info.password,
                pw_info.salt
            );

            AddUserToDatabase(user_info, member);
        }
        #endregion

        #region Database
        private void AddUserToDatabase(UserInfo user_info, Member member)
        {
            string query = string.Format("insert into `Member`(`Name`, Sex) values(@Name, @Sex); " +
                "insert into `Account`(MemberID, Username, PasswordHash, PasswordSalt) values(LAST_INSERT_ID(), @Username, @Hash, @Salt);");
            MySqlParameter[] param = new MySqlParameter[5];
            param[0] = new MySqlParameter("@Name", member.Name);
            param[1] = new MySqlParameter("@Sex", member.Sex);
            param[2] = new MySqlParameter("@Username", user_info.Username);
            param[3] = new MySqlParameter("@Hash", user_info.PasswordHash);
            param[4] = new MySqlParameter("@Salt", user_info.Salt);

            DBConnection db = new DBConnection();
            db.ExecuteInsertUpdateDeleteQuery(query, param);
        }
        #endregion

        private (byte[] password, byte[] salt) GenerateHashedPasswordAndSalt(string password)
        {
            // Generate a new salt
            var rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[salt_size];
            rng.GetBytes(salt);

            // Hash password with salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, pbkdf2_iterations);
            byte[] password_hash = pbkdf2.GetBytes(hash_size);

            return (password_hash, salt);
        }

        private bool VerifyPassword(string input_pw, byte[] salt, byte[] hashed_pw)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(input_pw, salt, pbkdf2_iterations);
            byte[] input_pw_hash = pbkdf2.GetBytes(hash_size);

            return input_pw_hash.SequenceEqual(hashed_pw);
        }
    }
}
