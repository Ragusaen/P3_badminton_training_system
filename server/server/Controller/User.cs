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
        private const int pbkdf2Iterations = 100000;
        private const int hashSize = 32;
        private const int saltSize = 128;
        private const int tokenSize = 64;

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
            return new UserInfo(dt.Rows[0][0].ToString(), (byte[])dt.Rows[0][1], (byte[])dt.Rows[0][2]);
        }

        private byte[] GenerateLoginToken()
        {
            // Generate a random token for the user to login with
            var rng = new RNGCryptoServiceProvider();
            byte[] token = new byte[tokenSize];
            rng.GetBytes(token);
            return token;
        }
        #endregion

        #region Create
        public void Create(string username, string password, Member member)
        {
            var pw_info = GenerateHashedPasswordAndSalt(password);
            UserInfo userInfo = new UserInfo(
                username,
                pw_info.password,
                pw_info.salt
            );

            AddUserToDatabase(userInfo, member);
        }
        #endregion

        #region Database
        private void AddUserToDatabase(UserInfo userInfo, Member member)
        {
            string query = string.Format("insert into `Member`(`Name`, Sex) values(@Name, @Sex); " +
                "insert into `Account`(MemberID, Username, PasswordHash, PasswordSalt) values(LAST_INSERT_ID(), @Username, @Hash, @Salt);");
            MySqlParameter[] param = new MySqlParameter[5];
            param[0] = new MySqlParameter("@Name", member.Name);
            param[1] = new MySqlParameter("@Sex", member.Sex);
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
            byte[] salt = new byte[saltSize];
            rng.GetBytes(salt);

            // Hash password with salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, pbkdf2Iterations);
            byte[] passwordHash = pbkdf2.GetBytes(hashSize);

            return (passwordHash, salt);
        }

        private bool VerifyPassword(string inputPw, byte[] salt, byte[] hashedPw)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(inputPw, salt, pbkdf2Iterations);
            byte[] inputPwHash = pbkdf2.GetBytes(hashSize);

            return inputPwHash.SequenceEqual(hashedPw);
        }
    }
}
