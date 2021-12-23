using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_Reseau_app
{
    public class Controller
    {
        public View view { get; }

        private FormList formList;
        private FormSearch formSearch;
        private Database db;

        private bool connected;

        public Controller(View view_controller)
        {
            view = view_controller;
            view.controller = this;
            db = new Database();
            connected = db.IsConnect();
            if (connected == false)
            {
                view.DisplayError("La connection à la db a échoué");
            }
            else
            {
                view.DisplayError("Tout fonctionne bien");
            }
        }

        public void checkRegion(string region)
        {
            if (region != "")
            {
                db.AddRegion(region);
            }
        }

        public void DisplayRegions()
        {
            view.DisplayRegions(db.GetRegions());
        }

        public void DisplayLocations()
        {
            view.DisplayLocations(db.GetLocations());
        }

        public void DisplayFormList(Form form)
        {
            formList = new FormList();
            formList.Show();
            form.Visible = false;
            formList.controller = this;
            DisplayList();
        }

        public void DisplayList()
        {
            formList.FillTable(db.GetEmployees());
        }

        public void DisplayFormSearch(Form form)
        {
            formSearch = new FormSearch();
            formSearch.Show();
            form.Visible = false;
            formSearch.controller = this;
        }

        public List<string[]> SearchEmployee(string text)
        {
            return db.SearchEmployeeByName(text);
        }
    }
}
