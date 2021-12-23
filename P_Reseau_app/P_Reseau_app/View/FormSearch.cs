using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P_Reseau_app
{
    public partial class FormSearch : Form
    {
        public Controller controller;
        private DataGridView dataGridView;
        private Panel panel1;

        public FormSearch()
        {
            InitializeComponent();
            dataGridView = new DataGridView();
            panel1 = new Panel();
        }

        private void ListeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.DisplayFormList(this);
        }

        private void RechercheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.DisplayFormSearch(this);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DisplaySearchResult(controller.SearchEmployee(textBox1.Text));
        }

        private void DisplaySearchResult(List<string[]> stuffThatWeSearched)
        {
            panel1.Controls.Remove(dataGridView);
            dataGridView = null;
            dataGridView = new DataGridView();
            this.Controls.Add(panel1);
            panel1.Width = this.Width - 4;
            panel1.Height = this.Height - 4;
            panel1.Location = new Point(0, textBox1.Height + menuStrip1.Height);
            panel1.Controls.Add(dataGridView);
            dataGridView.Size = new Size(panel1.Width, panel1.Height);
            label1.Visible = false;
            textBox1.Location = new Point(0, menuStrip1.Height);
            button1.Location = new Point(textBox1.Width, menuStrip1.Height);
            button1.Height = textBox1.Height;
            FillTable(stuffThatWeSearched);
            
        }

        public void FillTable(List<string[]> employees)
        {
            dataGridView.ColumnCount = 11;
            //ouais genre frero faut ajouet le type int pour les id pour bien des faire des bails
            dataGridView.Columns[0].Name = "employee_id";
            dataGridView.Columns[1].Name = "first_name";
            dataGridView.Columns[2].Name = "last_name";
            dataGridView.Columns[3].Name = "email";
            dataGridView.Columns[4].Name = "phone_number";
            dataGridView.Columns[5].Name = "hire_date";
            dataGridView.Columns[6].Name = "job_id";
            dataGridView.Columns[7].Name = "salary";
            dataGridView.Columns[8].Name = "commission_pct";
            dataGridView.Columns[9].Name = "department_id";
            dataGridView.Columns[10].Name = "Options";

            foreach (string[] employee in employees)
            {
                dataGridView.Rows.Add(employee);

            }
        }
    }
}
