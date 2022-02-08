using Car_Data_Application.Models;
using Car_Data_Application.Models.Vehicle_Classes;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Car_Data_Application.Controllers
{
    class RefuelingHistoryContentGenerator : CarDataAppController
    {
        public void GeneratorRefulingHistory(MainWindow mw, User user, RefuelingHistoryPage translation)
        {
            InitialAssignValue(mw, user);

            Grid MainGrid = new Grid();

            int index = 0;
            foreach (Refueling refueling in user.Vehicles[user.ActiveCarIndex].Refulings)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(120) });

                Grid RefuelingGrid = new Grid();
                SetGridProps(ref RefuelingGrid, index);

                RefuelingGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(114) });
                RefuelingGrid.ColumnDefinitions.Add(new ColumnDefinition());


                RefuelingGrid.Children.Add(GenerateIcon("../../../Images/Icons/gas-station.png", 0, 0));

                Grid RefuelingGridContent = new Grid();
                Grid.SetColumn(RefuelingGridContent, 1);
                RefuelingGridContent.HorizontalAlignment = HorizontalAlignment.Right;
                RefuelingGridContent.VerticalAlignment = VerticalAlignment.Center;

                for (int x = 0; x <= 5; x++) //5 is number of rows
                {
                    RefuelingGridContent.RowDefinitions.Add(new RowDefinition());
                }
                for (int y = 0; y <= 3; y++) //3 is number of columns
                {
                    RefuelingGridContent.ColumnDefinitions.Add(new ColumnDefinition());
                }

                string LightTextColor = "#FF9C9397";
                string DarkTextColor = "#FF2A2729"; // change to set in config

                switch (PUser.UserLanguage)
                {
                    case "PL":
                        RefuelingGridContent.Children.Add(GenerateTextBlock(translation.RefuelingCost.PL, 1, 0, LightTextColor, HorizontalAlignment.Right));
                        RefuelingGridContent.Children.Add(GenerateTextBlock(translation.AmountOfFuel.PL, 2, 0, LightTextColor, HorizontalAlignment.Right));
                        RefuelingGridContent.Children.Add(GenerateTextBlock(translation.PricePerLiter.PL, 3, 0, LightTextColor, HorizontalAlignment.Right));
                        RefuelingGridContent.Children.Add(GenerateTextBlock(translation.Consumption.PL, 4, 0, LightTextColor, HorizontalAlignment.Right));
                        break;

                    case "ENG":
                        RefuelingGridContent.Children.Add(GenerateTextBlock(translation.RefuelingCost.ENG, 1, 0, LightTextColor, HorizontalAlignment.Right));
                        RefuelingGridContent.Children.Add(GenerateTextBlock(translation.AmountOfFuel.ENG, 2, 0, LightTextColor, HorizontalAlignment.Right));
                        RefuelingGridContent.Children.Add(GenerateTextBlock(translation.PricePerLiter.ENG, 3, 0, LightTextColor, HorizontalAlignment.Right));
                        RefuelingGridContent.Children.Add(GenerateTextBlock(translation.Consumption.ENG, 4, 0, LightTextColor, HorizontalAlignment.Right));
                        break;
                }

                RefuelingGridContent.Children.Add(GenerateTextBlock(refueling.Date.ToString(), 0, 2, DarkTextColor, HorizontalAlignment.Right, VerticalAlignment.Top));

                RefuelingGridContent.Children.Add(GenerateTextBlock(refueling.CarMillage.ToString() + " km", 1, 2, DarkTextColor, HorizontalAlignment.Right, VerticalAlignment.Bottom));

                RefuelingGridContent.Children.Add(GenerateTextBlock("+ " + refueling.DistanceFromTheLastRefueling.ToString() + " km", 2, 2, LightTextColor, HorizontalAlignment.Right, VerticalAlignment.Top));

                RefuelingGridContent.Children.Add(GenerateTextBlock(refueling.TotalPrice.ToString() + " zł", 1, 1));

                RefuelingGridContent.Children.Add(GenerateTextBlock(refueling.PriceForLiter.ToString() + " zł", 2, 1));

                RefuelingGridContent.Children.Add(GenerateTextBlock(refueling.Liters.ToString() + " L", 3, 1));

                RefuelingGridContent.Children.Add(GenerateTextBlock(refueling.Consumption.ToString() + " L/100km", 4, 1));

                RefuelingGridContent.Children.Add(GenerateTextBlock(refueling.FuelType.ToString(), 3, 2, LightTextColor, HorizontalAlignment.Right));

                RefuelingGrid.Children.Add(RefuelingGridContent);

                MainGrid.Children.Add(RefuelingGrid);

                index++;
            }            
            mw.ScrollViewerContent.Content = MainGrid;
        }

        private void InitialAssignValue(MainWindow mw, User user)
        {
            mw.WhereAreYou = "RefuelingHistoryPage";
            mainWindow = mw;
            PUser = user;
            SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.FindName("SidePanel")));
        }
    }
}
