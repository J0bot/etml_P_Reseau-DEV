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
            this.StartPosition = FormStartPosition.CenterScreen;
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
            dataGridView.ColumnCount = 13;

            dataGridView.Columns[0].Name = "employee_id";
            dataGridView.Columns[1].Name = "first_name";
            dataGridView.Columns[2].Name = "last_name";
            dataGridView.Columns[3].Name = "email";
            dataGridView.Columns[4].Name = "phone_number";
            dataGridView.Columns[5].Name = "hire_date";
            dataGridView.Columns[6].Name = "job_title";
            dataGridView.Columns[7].Name = "salary";
            dataGridView.Columns[8].Name = "commission_pct";
            dataGridView.Columns[9].Name = "department_name";
            dataGridView.Columns[9].Width = 150;
            dataGridView.Columns[10].Name = "Detail";
            dataGridView.Columns[10].Width = 40;
            dataGridView.Columns[11].Name = "Modify";
            dataGridView.Columns[11].Width = 45;
            dataGridView.Columns[12].Name = "Delete";
            dataGridView.Columns[12].Width = 40;

            foreach (string[] employee in employees)
            {
                dataGridView.Rows.Add(employee);

            }

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DataGridViewButtonCell btnDetail = new DataGridViewButtonCell();
                DataGridViewButtonCell btnDelete = new DataGridViewButtonCell();
                DataGridViewButtonCell btnModify = new DataGridViewButtonCell();
                DataGridViewCellStyle btnStyle = new DataGridViewCellStyle();
                btnStyle.Font = new Font("Wingdings 2", 10f, FontStyle.Bold);
                btnDetail.Style = btnStyle;
                btnDelete.Style = btnStyle;
                btnModify.Style = btnStyle;

                btnDetail.Value = "2";
                btnDelete.Value = "3";
                btnModify.Value = "!";



                row.Cells[10] = btnDetail;
                row.Cells[11] = btnModify;
                row.Cells[12] = btnDelete;

            }
        }
    }
}
