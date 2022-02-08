using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Car_Data_Application.Controllers
{
    class AddCostPageGenerator : CarDataAppController
    {
        public void PageGenerator(MainWindow mw, User user, Config paramConfig)
        {
            // TITLE // TYPE // DATA // KOSZT // COMENT // NEGATIV COTS // MILAGE(OPTIONAL)
            InitialAssignValue(mw, user, paramConfig);
        }

        private void InitialAssignValue(MainWindow mw, User user, Config paramConfig)
        {
            config = paramConfig;
            mainWindow = mw;
            PUser = user;
            mainWindow.AddButon.Visibility = Visibility.Hidden;
            mainWindow.WhereAreYou = "AddCostPage";
            SetButtonColor("CostsPage", ((Grid)mainWindow.FindName("SidePanel")));
        }

        private Grid ContentGenerator ()
        {
            return null;
        }
    }
}
