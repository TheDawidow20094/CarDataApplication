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
            RefreshPage(Config.MainPanel.HomePage);
        }

        private void RefreshPage(HomePage homePage)
        { 
            switch (mainWindow.WhereAreYou)
            {
                case "HomePage":
                    HomeContentGenerator RefreshHomePage = new HomeContentGenerator();
                    RefreshHomePage.GeneratorHomeContent(mainWindow, PUser, homePage);
                break;

                case "CostsPage":
                    CostContentGenerator RefreshCostPage = new CostContentGenerator();
                    RefreshCostPage.CostGenerator(mainWindow, PUser);
                break;

                case "RefuelingHistoryPage":
                    RefuelingHistoryContentGenerator RefreshRefuelinfHistoryPage = new RefuelingHistoryContentGenerator();
                    RefreshRefuelinfHistoryPage.GeneratorRefulingHistory(mainWindow, PUser);
                break;

                case "CalculatorPage":
                    CalculatorContentGenerator RefreshCalculatorPage = new CalculatorContentGenerator();
                    RefreshCalculatorPage.CalculatorGenerator(mainWindow, PUser);
                break;
            }
        }
    }
}
