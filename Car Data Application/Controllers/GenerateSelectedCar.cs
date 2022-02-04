using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Car_Data_Application.Controllers
{
    class GenerateSelectedCar : CarDataAppController
    {
        private ComboBox Carlist = new ComboBox();

        public void GeneratorCarSelectList(MainWindow mw, User user, MainGrid config)
        {
            InitialAssignValue(mw, user, config);

            foreach (Vehicle vehicle in user.Vehicles)
            {
                Carlist.Items.Add(vehicle.Brand + " " + vehicle.Model);
            }

            mw.CarName.Children.Add(Carlist);        
        }

        private void InitialAssignValue(MainWindow mw, User user, MainGrid config)
        {
            mainWindow = mw;
            PUser = user;
            Config = config;
            Carlist.Background = Brushes.LightCoral;
            Carlist.HorizontalContentAlignment = HorizontalAlignment.Center;
            Carlist.VerticalContentAlignment = VerticalAlignment.Center;
            Carlist.SelectionChanged += CarlistSelectionChanged;
        }

        private void CarlistSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PUser.ActiveCarIndex = Carlist.SelectedIndex;
            PUser.SerializeData();
            RefreshPage(Config);
        }

        private void RefreshPage(MainGrid config)
        { 
            switch (mainWindow.WhereAreYou)
            {
                case "HomePage":
                    new HomeContentGenerator().GeneratorHomeContent(mainWindow, PUser, config.MainPanel.HomePage);
                break;

                case "CostsPage":
                    new CostContentGenerator().CostGenerator(mainWindow, PUser);
                break;

                case "RefuelingHistoryPage":
                    new RefuelingHistoryContentGenerator().GeneratorRefulingHistory(mainWindow, PUser, config.MainPanel.RefuelingHistoryPage);
                break;

                case "CalculatorPage":
                    new CalculatorContentGenerator().CalculatorGenerator(mainWindow, PUser, Config);
                break;

                case "AddRefuelingPage":
                    new AddRefuelingPageGenerator().PageGenerator(mainWindow, PUser, Config);
                break;
            }
        }
    }
}
