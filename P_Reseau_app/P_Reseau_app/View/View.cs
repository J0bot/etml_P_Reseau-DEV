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
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public void DisplayError(string text)
        {
            errorLabel.Text = text;
        }

        private void ListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.DisplayFormList(this);
        }

        private void RechercherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.DisplayFormSearch(this);

        }
    }
}
