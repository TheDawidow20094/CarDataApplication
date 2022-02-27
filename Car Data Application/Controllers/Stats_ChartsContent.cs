using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using Car_Data_Application.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Car_Data_Application.Controllers
{
    class Stats_ChartsContent : CarDataAppController
    {
        private StatsChartsPage PTranslation;

        public void PageGenerator(MainWindow mw, User user)
        {
            InitialAssignValue(mw, user);

            Grid MainGrid = new();

            FuelDataChar GenerateChar = new();
            //GenerateChar.InitializeComponent();
            MainGrid.Children.Add(GenerateChar);
            mainWindow.ScrollViewerContent.Content = MainGrid;
        }
        
        private void InitialAssignValue(MainWindow mw, User user)
        {
            mainWindow = mw;
            PUser = user;
            mainWindow.AddButon.Visibility = Visibility.Hidden;
            mainWindow.WhereAreYou = "DataStatsPage";
            SetButtonColor(mainWindow.WhereAreYou, (Grid)mainWindow.FindName("SidePanel"));
        }
    }
}
