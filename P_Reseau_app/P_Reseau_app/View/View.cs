using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace P_Reseau_app
{
    public partial class View : Form
    {
        public View()
        {
            InitializeComponent();

            Database conn = new Database();

            ////TOuT METTRE çA DANS UN FICHIER .JSON
            //conn.Server = "172.20.10.5";
            //conn.DatabaseName = "db_employees";
            //conn.UserName = "remote";
            //conn.Password = ".Etml123";
            if (conn.IsConnect())
            {
                List<string[]> regions = conn.GetRegions();
                foreach (string[] region in regions)
                {
                    foreach (string item in region)
                    {
                        debugLabel1.Text += item;

                    }
                }

                /*
                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = "SELECT * FROM regions GROUP BY region_name";
                var cmd = new MySqlCommand(query, conn.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string someStringFromColumnZero = reader.GetString(0);
                    string someStringFromColumnOne = reader.GetString(1);
                    debugLabel1.Text = someStringFromColumnZero + "," + someStringFromColumnOne;
                    //System.Diagnostics.Debug.WriteLine(someStringFromColumnZero + "," + someStringFromColumnOne);

                }*/
                conn.Close();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Database conn = new Database();
            //conn.Server = "172.20.10.5";
            //conn.DatabaseName = "db_employees";
            //conn.UserName = "remote";
            //conn.Password = ".Etml123";
            if (conn.IsConnect())
            {
                debugLabel1.Text += this.textBox1.Text;
                conn.AddRegion(this.textBox1.Text);
            }


        }
    }
}
