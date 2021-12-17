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
        /// Execute une query qui va retourner des données
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<string[]> ExecuteQueryList(string query)
        {
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
            reader.Close();
            return data;
        }

        /// <summary>
        /// Execute une query simple sans return;
        /// </summary>
        /// <param name="query"></param>
        public void ExecuteQuerySimple(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, this.Connection);
            cmd.ExecuteReader();
            
        }


        /// <summary>
        /// Retourne les region
        /// </summary>
        /// <returns>Les régions dans ce format : List<string[region_id,region_name]></returns>
        public List<string[]> GetRegions()
        {
            string query = "SELECT region_id ,region_name FROM regions";
            return ExecuteQueryList(query);
        }

        /// <summary>
        /// Ajoute des régions
        /// </summary>
        /// <param name="name"></param>
        public void AddRegion(string regionName)
        {
            string query = string.Format("INSERT INTO regions SET region_name=\"{0}\";",regionName );
            ExecuteQuerySimple(query);
        }

        public List<string[]> GetCountries()
        {
            string query = "SELECT country_id, country_name ,region_id FROM countries";
            return ExecuteQueryList(query);
        }

        public void AddCountry(string countryName, int RegionId)
        {
            string query = string.Format("INSERT INTO countries SET country_name=\"{0}\", region_id=\"{1}\";",countryName,RegionId);
            ExecuteQuerySimple(query);
        }

        public List<string[]> GetDeparments()
        {
            string query = "SELECT department_id ,department_name,location_id FROM departments";
            return ExecuteQueryList(query);
        }

        public void AddDepartment()
        {

        }

        public List<string[]> GetEmployees()
        {
            string query = "SELECT employee_id ,first_name,last_name,email,phone_number,hire_date,job_id,salary,commission_pct,department_id FROM employees";
            return ExecuteQueryList(query);
        }

        public void AddEmployee()
        {

        }

        public List<string[]> GetJobs()
        {
            string query = "SELECT job_id ,job_title,min_salary,max_salary FROM jobs";
            return ExecuteQueryList(query);
        }

        public void AddJob()
        {

        }

        public List<string[]> GetJob_history()
        {
            string query = "SELECT employee_id ,start_date,end_date,job_id,department_id FROM job_history";
            return ExecuteQueryList(query);
        }

        public void AddJob_history()
        {

        }

        public List<string[]> GetLocations()
        {
            string query = "SELECT location_id ,street_address,postal_code,city,state_province,country_id FROM locations";
            return ExecuteQueryList(query);
        }

        public void AddLocation()
        {

        }
    }
}
