﻿using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            System.Diagnostics.Debug.WriteLine("shit");
            var conn = Database.Instance();

            //TOuT METTRE çA DANS UN FICHIER .JSON
            conn.Server = "172.20.10.5";
            conn.DatabaseName = "db_employees";
            conn.UserName = "remote";
            conn.Password = ".Etml123";
            if (conn.IsConnect())
            {
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

                }
                conn.Close();
            }
        }
    }
}
