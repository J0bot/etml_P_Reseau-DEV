using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.IO;
using Newtonsoft.Json;

namespace P_Reseau_app
{
    public class Database
    {
        public Database()
        {
            using (StreamReader r = new StreamReader("Model/config.json"))
            {
                string json = r.ReadToEnd();
                ConfItems item = JsonConvert.DeserializeObject<ConfItems>(json);
                this.Server = item.Server;
                this.DatabaseName = item.DatabaseName;
                this.UserName = item.UserName;
                this.Password = item.Password;
            }
        }

        public Database(string server, string databaseName, string userName, string password)
        {
            this.Server = server;
            this.DatabaseName = databaseName;
            this.UserName = userName;
            this.Password = password;
        }

        private string Server { get; set; }
        private string DatabaseName { get; set; }
        private string UserName { get; set; }
        private string Password { get; set; }

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

        /// <summary>
        /// Retourne les region
        /// </summary>
        /// <returns>Les régions dans ce format : List<string[region_id,region_name]></returns>
        public List<string[]> GetRegions()
        {
            string query = "SELECT region_id , region_name FROM regions";
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

        public void AddRegion(string name)
        {
            string query = "INSERT INTO regions SET region_name=\""+name+"\";";
            new MySqlCommand(query, this.Connection);
            MySqlCommand cmd = new MySqlCommand(query, this.Connection);
            cmd.ExecuteReader();
        }



    }
}
