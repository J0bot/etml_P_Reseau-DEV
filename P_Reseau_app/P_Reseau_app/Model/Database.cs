using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Linq;

namespace P_Reseau_app
{
    public class Database
    {
        public Database()
        {

        }

        public Database(string server, string databaseName, string userName, string password)
        {
            this.Server = server;
            this.DatabaseName = databaseName;
            this.UserName = userName;
            this.Password = password;
        }

        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        private MySqlConnection Connection { get; set; }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(DatabaseName))
                    return false;
                string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, DatabaseName, UserName, Password);
                Connection = new MySqlConnection(connstring);
                Connection.Open();
            }

            return true;
        }

        public void Close()
        {
            Connection.Close();
        }

        public List<string[]> GetRegions()
        {
            string query = "SELECT * FROM regions";
            MySqlCommand cmd = new MySqlCommand(query, this.Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<string[]> data = new List<string[]>();

            int rowCount = reader.FieldCount;

            while (reader.Read())
            {
                string[] rowData = new string[rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    rowData[i] = reader.GetString(i);
                   // data[lineCount,i]= reader.GetString(i);
                }

                data.Add(rowData);
            }
            return data;
        }

    }
}
