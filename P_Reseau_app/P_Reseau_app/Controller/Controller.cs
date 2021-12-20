namespace P_Reseau_app
{
    public class Controller
    {
        public View view { get; }
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


    }
}
