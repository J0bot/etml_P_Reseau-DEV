using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_Reseau_app
{
    public class Controller
    {
        public View view;
        private Database db;

        private bool connected;

        public Controller(View view_controller)
        {
            view = view_controller;
            view.controller = this;
            db = new Database();
            connected = db.IsConnect();
            if(connected==false)
            {
                view.DisplayError("AYYYYYY");
            }
            else
            {
                view.DisplayError("oklm");
            }
        }

        public void checkRegion()
        {
            
        }

        public void DisplayRegions()
        {
            view.DisplayRegions(db.GetRegions());
        }



    }
}
