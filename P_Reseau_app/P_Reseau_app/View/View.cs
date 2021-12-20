using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            controller.DisplayLocations();
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

                debugLabel1.Text += region[1];
                debugLabel1.Text += "\n";

            }
        }

        public void DisplayLocations(List<string[]> locations)
        {
            labelStreetAddress.Text = "";
            labelCity.Text = "";
            labelCountryName.Text = "";

            foreach (string[] location in locations)
            {
                labelStreetAddress.Text += location[1] + "\n";
                labelCity.Text += location[3] + "\n";
                labelCountryName.Text += location[6] + "\n";
            }
        }
    }
}
