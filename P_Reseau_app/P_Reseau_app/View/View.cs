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
        public Controller controller;
        public View()
        {
            InitializeComponent();         
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            controller.DisplayRegions();
        }

        public void DisplayError(string text)
        {
            errorLabel.Text = text;
        }

        public void DisplayRegions(List<string[]> regions)
        {
            debugLabel1.Text = "";

            foreach (string[] region in regions)
            {
                foreach (string item in region)
                {
                    debugLabel1.Text += item;
                }
            }
        }
    }
}
