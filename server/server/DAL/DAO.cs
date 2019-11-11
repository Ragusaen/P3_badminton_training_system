using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server.DAL
{
    abstract class DAO<T> where T : new()
    {
        protected string PrimaryKeyName;
        protected string TableName;
        protected Dictionary<string, string> FieldColumnDictionary;

        public T LoadFields(List<string> fields, string primaryKeyValue)
        {
            if (fields.Count == 0)
                throw new ArgumentException("Attempted to get 0 fields");

            DBConnection dbcon = new DBConnection();

            var columns = fields.Select(f => FieldColumnDictionary[f]).ToList();

            string query = "SELECT @0";

            for (int i = 1; i < columns.Count; i++)
                query += $", @{i}";

            query += $" FROM `{TableName}` WHERE `{PrimaryKeyName}`=`{primaryKeyValue}`";

            Console.WriteLine(query);

            MySqlParameter[] parameters = new MySqlParameter[columns.Count];
            for (int i = 0; i < columns.Count; i++)
                parameters[i] = new MySqlParameter($"@{i}", columns[i]);

            DataTable dt = dbcon.ExecuteSelectQuery(query, parameters);

            var items = dt.Rows[0].ItemArray;

            T t = new T();

            for (int i = 0; i < columns.Count; i++)
            {
                typeof(T).GetField(fields[i]).SetValue(t, items[i]);
            }

            return t;
        }
    }
}
