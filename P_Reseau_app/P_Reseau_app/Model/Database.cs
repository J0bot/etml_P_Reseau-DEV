using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace P_Reseau_app
{
    public class Database
    {
        private string Server { get; set; }
        private string DatabaseName { get; set; }
        private string UserName { get; set; }
        private string Password { get; set; }

        private MySqlConnection Connection { get; set; }

        //C'est le mot de passe pour l'encryption et decryption
        private string passPhrase = ".Etml123";


        public Database()
        {
            using (StreamReader r = new StreamReader("Model/config.json"))
            {
                string json = r.ReadToEnd();
                ConfItems item = JsonConvert.DeserializeObject<ConfItems>(json);
                this.Server = item.Server;
                this.DatabaseName = item.DatabaseName;
                this.UserName = item.UserName;
                this.Password = StringCipher.Decrypt(item.Password, passPhrase);
            }
        }

        public Database(string server, string databaseName, string userName, string password)
        {
            this.Server = server;
            this.DatabaseName = databaseName;
            this.UserName = userName;
            this.Password = password;
        }

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

        #region Regions

        /// <summary>
        /// Retourne les region
        /// </summary>
        /// <returns>Les régions dans ce format : List<string[region_id,region_name]></returns>
        public List<string[]> GetRegions()
        {
            string query = "SELECT region_id ,region_name FROM regions";
            return ExecuteQueryList(query);
        }

        public List<string[]> GetRegion(string regionName)
        {
            string query = $"SELECT region_id ,region_name FROM regions WHERE region_name=\"{regionName}\"";
            return ExecuteQueryList(query);
        }

        public List<string[]> GetRegion(int regionId)
        {
            string query = $"SELECT region_id ,region_name FROM regions WHERE region_id=\"{regionId}\"";
            return ExecuteQueryList(query);
        }

        /// <summary>
        /// Ajoute des régions
        /// </summary>
        /// <param name="name"></param>
        public void AddRegion(string regionName)
        {
            string query = $"INSERT INTO regions SET region_name=\"{regionName}\";";
            ExecuteQuerySimple(query);
        }

        public void DeleteRegion(string regionName)
        {
            string query = $"DELETE FROM regions WHERE region_name=\"{regionName}\";";
            ExecuteQuerySimple(query);
        }

        public void DeleteRegion(int regionId)
        {
            string query = $"DELETE FROM regions WHERE region_id=\"{regionId}\";";
            ExecuteQuerySimple(query);
        }

        #endregion
        #region Countries

        /// <summary>
        /// Retourne tous les pays
        /// </summary>
        /// <returns></returns>
        public List<string[]> GetCountries()
        {
            string query = "SELECT country_id, country_name ,region_id, region_name FROM countries NATURAL JOIN regions";
            return ExecuteQueryList(query);
        }

        public List<string[]> GetCountry(string countryName)
        {
            string query = $"SELECT country_id, country_name ,region_id, region_name FROM countries NATURAL JOIN regions WHERE country_name=\"{countryName}\"";
            return ExecuteQueryList(query);
        }

        public List<string[]> GetCountry(int countryId)
        {
            string query = $"SELECT country_id, country_name ,region_id, region_name FROM countries NATURAL JOIN regions WHERE country_id=\"{countryId}\"";
            return ExecuteQueryList(query);
        }

        /// <summary>
        /// Ajoute un pays
        /// </summary>
        /// <param name="countryName"></param>
        /// <param name="RegionId"></param>
        public void AddCountry(string countryName, int RegionId)
        {
            string query = $"INSERT INTO countries SET country_name=\"{countryName}\", region_id=\"{RegionId}\";";
            ExecuteQuerySimple(query);
        }

        public List<string[]> DeleteCountry(string countryName)
        {
            string query = $"DELETE FROM countries WHERE country_name=\"{countryName}\"";
            return ExecuteQueryList(query);
        }

        public List<string[]> DeleteCountry(int countryId)
        {
            string query = $"DELETE FROM countries WHERE country_id=\"{countryId}\"";
            return ExecuteQueryList(query);
        }
        #endregion
        #region Departments

        public List<string[]> GetDepartments()
        {
            string query = "SELECT department_id ,department_name,location_id FROM departments";
            return ExecuteQueryList(query);
        }

        public List<string[]> GetDepartment(string departmentName)
        {
            string query = $"SELECT department_id ,department_name,location_id FROM departments WHERE department_name=\"{departmentName}\"";
            return ExecuteQueryList(query);
        }

        public List<string[]> GetDepartment(int departmentId)
        {
            string query = $"SELECT department_id ,department_name,location_id FROM departments WHERE department_id=\"{departmentId}\"";
            return ExecuteQueryList(query);
        }

        public void AddDepartment(string departmentName, int locationId)
        {
            string query = $"INSERT INTO departments SET department_name=\"{departmentName}\", location_id=\"{locationId}\";";
            ExecuteQuerySimple(query);
        }


        public void DeleteDepartment(string departmentName)
        {
            string query = $"DELETE FROM departments WHERE department_name=\"{departmentName}\"";
            ExecuteQuerySimple(query);
        }

        public void DeleteDepartment(int departmentId)
        {
            string query = $"DELETE FROM departments WHERE department_id=\"{departmentId}\"";
            ExecuteQuerySimple(query);
        }

        #endregion
        #region Employees


        public List<string[]> GetEmployees()
        {
            string query = "SELECT employee_id ,first_name,last_name,email,phone_number,hire_date,job_id,salary,commission_pct,department_id FROM employees";
            return ExecuteQueryList(query);
        }

        public List<string[]> GetEmployee(int employeeId)
        {
            string query = $"SELECT employee_id ,first_name,last_name,email,phone_number,hire_date,job_id,salary,commission_pct,department_id FROM employees WHERE employee_id=\"{employeeId}\"";
            return ExecuteQueryList(query);
        }

        public void AddEmployee(string firstName, string lastName, string email, string phoneNumber, string hireDate, int jobId, double salary, double commissionPct, int departmentId)
        {
            string query = $"INSERT INTO employees SET first_name=\"{firstName}\", last_name=\"{lastName}\", email=\"{email}\", phone_number=\"{phoneNumber}\", hire_date=\"{hireDate}\", job_id=\"{jobId}\", salary=\"{salary}\", commission_pct=\"{commissionPct}\", department_id=\"{departmentId}\";";
            ExecuteQuerySimple(query);
        }

        public void DeleteEmployee(int employeeId)
        {
            string query = $"DELETE FROM employees WHERE employee_id=\"{employeeId}\"";
            ExecuteQueryList(query);
        }

        #endregion
        #region Jobs

        public List<string[]> GetJobs()
        {
            string query = "SELECT job_id ,job_title,min_salary,max_salary FROM jobs";
            return ExecuteQueryList(query);
        }

        public List<string[]> GetJob(int jobId)
        {
            string query = $"SELECT job_id ,job_title,min_salary,max_salary FROM jobs WHERE job_id=\"{jobId}\"";
            return ExecuteQueryList(query);
        }

        public List<string[]> GetJob(string jobTitle)
        {
            string query = $"SELECT job_id ,job_title,min_salary,max_salary FROM jobs WHERE job_title=\"{jobTitle}\"";
            return ExecuteQueryList(query);
        }

        public void AddJob(string jobTitle, double minSalary, double maxSalary)
        {
            string query = $"INSERT INTO departments SET job_title=\"{jobTitle}\", min_salary=\"{minSalary}\", max_salary=\"{maxSalary}\";";
            ExecuteQuerySimple(query);
        }

        public void AddJob(string jobTitle)
        {
            string query = $"INSERT INTO departments SET job_title=\"{jobTitle}\";";
            ExecuteQuerySimple(query);
        }

        public void DeleteJob(string jobTitle)
        {
            string query = $"DELETE FROM jobs WHERE job_title=\"{jobTitle}\"";
            ExecuteQueryList(query);
        }

        public void DeleteJob(int jobId)
        {
            string query = $"DELETE FROM jobs WHERE job_id=\"{jobId}\"";
            ExecuteQueryList(query);
        }

        #endregion
        #region Job_history


        public List<string[]> GetJob_history()
        {
            string query = "SELECT employee_id ,start_date,end_date,job_id,department_id FROM job_history";
            return ExecuteQueryList(query);
        }

        public List<string[]> GetJob_history(int employeeId)
        {
            string query = $"SELECT employee_id ,start_date,end_date,job_id,department_id FROM job_history WHERE employee_id=\"{employeeId}\"";
            return ExecuteQueryList(query);
        }

        public void AddJob_history(int employeeId, string startDate, string endDate, int jobId, int departmentId)
        {
            string query = $"INSERT INTO departments SET employee_id=\"{employeeId}\", start_date=\"{startDate}\",end_date=\"{endDate}\",job_id=\"{jobId}\",department_id=\"{departmentId}\";";
            ExecuteQuerySimple(query);
        }

        public void DeleteJob_History(int employeeId)
        {
            string query = $"DELETE FROM job_history WHERE employee_id=\"{employeeId}\"";
            ExecuteQueryList(query);
        }

        #endregion
        #region Locations


        /// <summary>
        /// Returns all the informations of a location
        /// </summary>
        /// <returns>[0] : location_id, [1] : street_address, [2] : postal_code, [3] : city, [4] : state_province, [5] : country_id , [6] : country_name</returns>
        public List<string[]> GetLocations()
        {
            string query = "SELECT location_id ,street_address,postal_code,city,state_province,country_id, country_name FROM locations NATURAL JOIN countries";
            return ExecuteQueryList(query);
        }

        public List<string[]> GetLocation(int locationId)
        {
            string query = $"SELECT location_id ,street_address,postal_code,city,state_province,country_id, country_name FROM locations NATURAL JOIN countries WHERE location_id=\"{locationId}\"";
            return ExecuteQueryList(query);
        }

        public void AddLocation(string streetAddress, string postalCode, string city, string stateProvince, string countryId)
        {
            string query = $"INSERT INTO departments SET street_address=\"{streetAddress}\",postal_code=\"{postalCode}\",city=\"{city}\",state_province=\"{stateProvince}\",country_id=\"{countryId}\";";
            ExecuteQuerySimple(query);
        }

        public void AddLocation(string city, string countryId)
        {
            string query = $"INSERT INTO departments SET city=\"{city}\",country_id=\"{countryId}\";";
            ExecuteQuerySimple(query);
        }

        public void DeleteLocation(int locationId)
        {
            string query = $"DELETE FROM locations WHERE location_id=\"{locationId}\"";
            ExecuteQueryList(query);
        }

        #endregion

    }
}
