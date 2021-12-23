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

        /// <summary>
        /// Retourne une région spécifique
        /// </summary>
        /// <param name="regionId">l'id de la région</param>
        /// <returns>une liste de string des valeurs de la région</returns>
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

        /// <summary>
        /// Supprime une ou des régions par leur nom
        /// </summary>
        /// <param name="regionName">le nom de région à supprimer</param>
        public void DeleteRegion(string regionName)
        {
            string query = $"DELETE FROM regions WHERE region_name=\"{regionName}\";";
            ExecuteQuerySimple(query);
        }

        /// <summary>
        /// Supprime une région par son ID
        /// </summary>
        /// <param name="regionId">l'ID de la région à supprimer</param>
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

        /// <summary>
        /// retourne un pays suivant le nom donné
        /// </summary>
        /// <param name="countryName">le nom du pays à retourner</param>
        /// <returns>une list de string contenant le valeurs du ou des pays</returns>
        public List<string[]> GetCountry(string countryName)
        {
            string query = $"SELECT country_id, country_name ,region_id, region_name FROM countries NATURAL JOIN regions WHERE country_name=\"{countryName}\"";
            return ExecuteQueryList(query);
        }

        /// <summary>
        /// Retourne un pays définit par son ID
        /// </summary>
        /// <param name="countryId">l'ID du pays à retourner</param>
        /// <returns>une liste de string contenant les infos du pays</returns>
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

        /// <summary>
        /// supprime un pays suivant son nom
        /// </summary>
        /// <param name="countryName">le nom du pays à supprimer</param>
        public void DeleteCountry(string countryName)
        {
            string query = $"DELETE FROM countries WHERE country_name=\"{countryName}\"";
            ExecuteQuerySimple(query);
        }

        public void DeleteCountry(int countryId)
        {
            string query = $"DELETE FROM countries WHERE country_id=\"{countryId}\"";
            ExecuteQuerySimple(query);
        }
        #endregion
        #region Departments

        /// <summary>
        /// retourne tous les départements
        /// </summary>
        /// <returns>une liste de strings contenants les infos des départements</returns>
        public List<string[]> GetDepartments()
        {
            string query = "SELECT department_id ,department_name,location_id FROM departments";
            return ExecuteQueryList(query);
        }

        /// <summary>
        /// retourne un département basé sur le nom donné
        /// </summary>
        /// <param name="departmentName">le nom du département à retourner</param>
        /// <returns>une liste de strings contenants les infos du département</returns>
        public List<string[]> GetDepartment(string departmentName)
        {
            string query = $"SELECT department_id ,department_name,location_id FROM departments WHERE department_name=\"{departmentName}\"";
            return ExecuteQueryList(query);
        }

        /// <summary>
        /// retourne un département basé sur son ID
        /// </summary>
        /// <param name="departmentId">l'ID du département à retourner</param>
        /// <returns>une liste de strings contenants les infos du département</returns>
        public List<string[]> GetDepartment(int departmentId)
        {
            string query = $"SELECT department_id ,department_name,location_id FROM departments WHERE department_id=\"{departmentId}\"";
            return ExecuteQueryList(query);
        }

        /// <summary>
        /// ajoute un département dans la database
        /// </summary>
        /// <param name="departmentName">le nom du département à ajouter</param>
        /// <param name="locationId">l'ID du département à ajouter</param>
        public void AddDepartment(string departmentName, int locationId)
        {
            string query = $"INSERT INTO departments SET department_name=\"{departmentName}\", location_id=\"{locationId}\";";
            ExecuteQuerySimple(query);
        }


        /// <summary>
        /// supprime un département suivant le nom donné
        /// </summary>
        /// <param name="departmentName">le nom du département à supprimer</param>
        public void DeleteDepartment(string departmentName)
        {
            string query = $"DELETE FROM departments WHERE department_name=\"{departmentName}\"";
            ExecuteQuerySimple(query);
        }

        /// <summary>
        /// supprime un département suivant son ID
        /// </summary>
        /// <param name="departmentId">l'ID du département à supprimer</param>
        public void DeleteDepartment(int departmentId)
        {
            string query = $"DELETE FROM departments WHERE department_id=\"{departmentId}\"";
            ExecuteQuerySimple(query);
        }

        #endregion
        #region Employees

        /// <summary>
        /// retourne tous les employés
        /// </summary>
        /// <returns>une liste de strings contenants les infos des employés</returns>
        public List<string[]> GetEmployees()
        {
            string query = "SELECT employee_id ,first_name,last_name,email,phone_number,hire_date,job_title,salary,commission_pct,department_name FROM employees NATURAL JOIN departments NATURAL JOIN jobs ORDER BY employee_id ASC";
            return ExecuteQueryList(query);
        }

        /// <summary>
        /// retourne un employé suivant l'ID donné
        /// </summary>
        /// <param name="employeeId">l'ID de l'employé à retourner</param>
        /// <returns>une liste de strings contenants les infos de l'employé</returns>
        public List<string[]> GetEmployee(int employeeId)
        {
            string query = $"SELECT employee_id ,first_name,last_name,email,phone_number,hire_date,job_id,salary,commission_pct,department_id FROM employees WHERE employee_id=\"{employeeId}\"";
            return ExecuteQueryList(query);
        }

        /// <summary>
        /// ajoute un employé à la base de données
        /// </summary>
        /// <param name="firstName">le prénom de l'employé</param>
        /// <param name="lastName">le nom de l'employé</param>
        /// <param name="email">l'email de l'employé</param>
        /// <param name="phoneNumber">le numéro de téléphone de l'employé</param>
        /// <param name="hireDate">la date d'embauchement de l'employé</param>
        /// <param name="jobId">l'ID du job de l'employé</param>
        /// <param name="salary">le salaire de l'employé</param>
        /// <param name="commissionPct">Le pourcentage de commission de l'employé</param>
        /// <param name="departmentId">l'ID du département de l'employé</param>
        public void AddEmployee(string firstName, string lastName, string email, string phoneNumber, string hireDate, int jobId, double salary, double commissionPct, int departmentId)
        {
            string query = $"INSERT INTO employees SET first_name=\"{firstName}\", last_name=\"{lastName}\", email=\"{email}\", phone_number=\"{phoneNumber}\", hire_date=\"{hireDate}\", job_id=\"{jobId}\", salary=\"{salary}\", commission_pct=\"{commissionPct}\", department_id=\"{departmentId}\";";
            ExecuteQuerySimple(query);
        }

        /// <summary>
        /// supprime un employé selon l'ID donné
        /// </summary>
        /// <param name="employeeId">l'ID de l'employé à supprimer</param>
        public void DeleteEmployee(int employeeId)
        {
            string query = $"DELETE FROM employees WHERE employee_id=\"{employeeId}\"";
            ExecuteQuerySimple(query);
        }

        public List<string[]> SearchEmployeeByName(string text)
        {
            string pattern = text + "%";
            string query = $"SELECT employee_id ,first_name,last_name,email,phone_number,hire_date,job_title,salary,commission_pct,department_name FROM employees  NATURAL JOIN departments NATURAL JOIN jobs WHERE first_name LIKE \"{pattern}\"  ORDER BY first_name ASC";
            return ExecuteQueryList(query);
        }

        #endregion
        #region Jobs

        /// <summary>
        /// retourne tous les jobs de la db
        /// </summary>
        /// <returns>une liste de strings contenant les infos des jobs</returns>
        public List<string[]> GetJobs()
        {
            string query = "SELECT job_id ,job_title,min_salary,max_salary FROM jobs";
            return ExecuteQueryList(query);
        }

        /// <summary>
        /// retourne un job selon son ID
        /// </summary>
        /// <param name="jobId">l'ID du job à retourner</param>
        /// <returns>Une liste de strings contenants les infos du job</returns>
        public List<string[]> GetJob(int jobId)
        {
            string query = $"SELECT job_id ,job_title,min_salary,max_salary FROM jobs WHERE job_id=\"{jobId}\"";
            return ExecuteQueryList(query);
        }

        /// <summary>
        /// retourne un job selon son nom
        /// </summary>
        /// <param name="jobTitle">le nom du job à retourner</param>
        /// <returns>une liste de strings contenants les infos du job</returns>
        public List<string[]> GetJob(string jobTitle)
        {
            string query = $"SELECT job_id ,job_title,min_salary,max_salary FROM jobs WHERE job_title=\"{jobTitle}\"";
            return ExecuteQueryList(query);
        }

        /// <summary>
        /// ajoute un job dans la database
        /// </summary>
        /// <param name="jobTitle">le nom du job à ajouter</param>
        /// <param name="minSalary">le salaire minimum du job à retourner</param>
        /// <param name="maxSalary">le salaire maximum du job à retourner</param>
        public void AddJob(string jobTitle, double minSalary, double maxSalary)
        {
            string query = $"INSERT INTO departments SET job_title=\"{jobTitle}\", min_salary=\"{minSalary}\", max_salary=\"{maxSalary}\";";
            ExecuteQuerySimple(query);
        }

        /// <summary>
        /// ajouter un job à la db sans spécifier le salaire
        /// </summary>
        /// <param name="jobTitle">le nom du job à ajouter</param>
        public void AddJob(string jobTitle)
        {
            string query = $"INSERT INTO departments SET job_title=\"{jobTitle}\";";
            ExecuteQuerySimple(query);
        }

        /// <summary>
        /// supprimer un job solon son nom
        /// </summary>
        /// <param name="jobTitle">le nom du job à supprimer</param>
        public void DeleteJob(string jobTitle)
        {
            string query = $"DELETE FROM jobs WHERE job_title=\"{jobTitle}\"";
            ExecuteQuerySimple(query);
        }

        /// <summary>
        /// supprime un job selon son ID
        /// </summary>
        /// <param name="jobId">l'ID du job à supprimer</param>
        public void DeleteJob(int jobId)
        {
            string query = $"DELETE FROM jobs WHERE job_id=\"{jobId}\"";
            ExecuteQuerySimple(query);
        }

        #endregion
        #region Job_history

        /// <summary>
        /// retourne l'historique des jobs
        /// </summary>
        /// <returns>une liste de strings contenant les infos de l'historique des jobs</returns>
        public List<string[]> GetJob_history()
        {
            string query = "SELECT employee_id ,start_date,end_date,job_id,department_id FROM job_history";
            return ExecuteQueryList(query);
        }

        /// <summary>
        /// retourne l'historique des jobs selon l'ID d'un employé
        /// </summary>
        /// <param name="employeeId">l'ID de l'employé</param>
        /// <returns>une liste de strings contenants les infos de l'historique des jobs</returns>
        public List<string[]> GetJob_history(int employeeId)
        {
            string query = $"SELECT employee_id ,start_date,end_date,job_id,department_id FROM job_history WHERE employee_id=\"{employeeId}\"";
            return ExecuteQueryList(query);
        }

        /// <summary>
        /// ajouter un hostorique des jobs à la database
        /// </summary>
        /// <param name="employeeId">l'ID de lemployé auquel la liste appartient</param>
        /// <param name="startDate">le début de l'historique des jobs</param>
        /// <param name="endDate">la fin de l'historique des jobs</param>
        /// <param name="jobId">l'id du job</param>
        /// <param name="departmentId">l'id du département</param>
        public void AddJob_history(int employeeId, string startDate, string endDate, int jobId, int departmentId)
        {
            string query = $"INSERT INTO departments SET employee_id=\"{employeeId}\", start_date=\"{startDate}\",end_date=\"{endDate}\",job_id=\"{jobId}\",department_id=\"{departmentId}\";";
            ExecuteQuerySimple(query);
        }

        /// <summary>
        /// supprimer un historique de job
        /// </summary>
        /// <param name="employeeId">l'ID de l'employé auquel l'historique est lié</param>
        public void DeleteJob_History(int employeeId)
        {
            string query = $"DELETE FROM job_history WHERE employee_id=\"{employeeId}\"";
            ExecuteQuerySimple(query);
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

        /// <summary>
        /// retourne une location par son ID
        /// </summary>
        /// <param name="locationId">l'id de la location à retourner</param>
        /// <returns>une liste de strings contenant les infos de la location</returns>
        public List<string[]> GetLocation(int locationId)
        {
            string query = $"SELECT location_id ,street_address,postal_code,city,state_province,country_id, country_name FROM locations NATURAL JOIN countries WHERE location_id=\"{locationId}\"";
            return ExecuteQueryList(query);
        }

        /// <summary>
        /// ajoute une location à la db
        /// </summary>
        /// <param name="streetAddress">l'addresse de rue de la location</param>
        /// <param name="postalCode">le code postal</param>
        /// <param name="city">la ville</param>
        /// <param name="stateProvince">la province</param>
        /// <param name="countryId">l'id du pays dans lequel est la location</param>
        public void AddLocation(string streetAddress, string postalCode, string city, string stateProvince, string countryId)
        {
            string query = $"INSERT INTO departments SET street_address=\"{streetAddress}\",postal_code=\"{postalCode}\",city=\"{city}\",state_province=\"{stateProvince}\",country_id=\"{countryId}\";";
            ExecuteQuerySimple(query);
        }

        /// <summary>
        /// ajouter une location just en spéficiant la ville et le pay
        /// </summary>
        /// <param name="city">le nom de la ville</param>
        /// <param name="countryId">l'id du pays</param>
        public void AddLocation(string city, string countryId)
        {
            string query = $"INSERT INTO departments SET city=\"{city}\",country_id=\"{countryId}\";";
            ExecuteQuerySimple(query);
        }

        /// <summary>
        /// supprime une location par son id
        /// </summary>
        /// <param name="locationId">l'id de la location à supprimer</param>
        public void DeleteLocation(int locationId)
        {
            string query = $"DELETE FROM locations WHERE location_id=\"{locationId}\"";
            ExecuteQuerySimple(query);
        }

        #endregion

    }
}
