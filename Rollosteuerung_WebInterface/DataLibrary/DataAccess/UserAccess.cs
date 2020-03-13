using DataLibrary.BusinessModel;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess
{
    public static class UserAccess
    {
        private static string strKey = "htlww";
        public static string GetConnectionString(string connectionName = "postgresql")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static bool UpdateUser(string email, string first, string last,bool isadmin,bool issuperadmin,string password)
        {
            int i = 0;
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnectionString()))
            {
                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand($"update users where email='{email} set last ' an;", conn);
                i = comm.ExecuteNonQuery();
            }
            if (i==1)
                return true;
            else
                return false;
        }

        public static UserModel GetUser(string email)
        {
            UserModel model = null;
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnectionString()))
            {
                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand($"select * from users where email='{email}';", conn);
                NpgsqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                    model = new UserModel(int.Parse(dr[0].ToString()), dr[5].ToString(), dr[1].ToString(), dr[2].ToString(), bool.Parse(dr[3].ToString()), bool.Parse(dr[4].ToString()),
                        dr[6].ToString());

            }

            if (model.ID != 0)
                return model;
            else
                return model;
        }
        public static List<UserModel> GetAllUsers()
        {
            List<UserModel> model = new List<UserModel>();
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnectionString()))
            {
                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand($"select * from users;", conn);
                NpgsqlDataReader dr = comm.ExecuteReader();

                while (dr.Read())
                    model.Add(new UserModel(int.Parse(dr[0].ToString()), dr[5].ToString(), dr[1].ToString(), dr[2].ToString(), bool.Parse(dr[3].ToString()), bool.Parse(dr[4].ToString()),
                        dr[6].ToString()));

            }

            return model;
        }
        public static bool DeleteUser(string email,string first,string last)
        {
            int i = 0;
            UserModel model = new UserModel();
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnectionString()))
            {
                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand($"delete from users where email='{email}' and firstname='{first}' and lastname='{last}'", conn);
                i = comm.ExecuteNonQuery();
                
            }
            if (i==1)
                return true;
            else
                return false;
        }
        public static bool CreateUser(string f, string l, bool isAdmin, bool isSuperAdmin, string email, string password)
        {
            int i = 0;
            UserModel model = new UserModel();
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnectionString()))
            {
                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand($"insert into users (firstname,lastname,is_admin,is_superadmin,email,password)" +
                    $"values('{f}', '{l}', {isAdmin.ToString()}, {isSuperAdmin.ToString()}, '{email}', '{password}');", conn);
                i = comm.ExecuteNonQuery();


            }
            if (i == 1)
                return true;
            else
                return false;
        }


        public static string Encrypt(string text)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = ASCIIEncoding.ASCII.GetBytes(text);
                return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
                    TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        public static string Decrypt(string text)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Convert.FromBase64String(text);
                string strDecrypted = ASCIIEncoding.ASCII.GetString
                (objDESCrypto.CreateDecryptor().TransformFinalBlock
                (byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;
                return strDecrypted;
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

    }
}
