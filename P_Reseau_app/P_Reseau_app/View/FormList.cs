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
    public partial class FormList : Form
    {
        public Controller controller;

        private DataGridView dataGridView;

        public FormList()
        {
            InitializeComponent();
            DisplayTable();
        }


        private void DisplayTable()
        {
            dataGridView = new DataGridView();
            panel1.Controls.Add(dataGridView);
            dataGridView.Size = new Size(panel1.Width, panel1.Height);

        }

        public void FillTable(List<string[]> employees)
        {
            dataGridView.ColumnCount = 11;

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

        private void ListeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.DisplayFormList(this);
        }
        private void RechercheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.DisplayFormSearch(this);
        }
    }
}
