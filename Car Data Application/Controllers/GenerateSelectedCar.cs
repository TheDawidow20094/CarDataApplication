using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace Car_Data_Application.Controllers
{
    class GenerateSelectedCar : CarDataAppController
    {
        private Storyboard EntryHeightAnimationStoryboard = new Storyboard();
        private Storyboard ExitHeightAnimationStoryboard = new Storyboard();

        public void GeneratorCarSelectList(MainWindow mw, User user, Config paramConfig)
        {
            InitialAssignValue(mw, user, paramConfig);

            Grid VehiclesNameListGrid = new();
            VehiclesNameListGrid.HorizontalAlignment = HorizontalAlignment.Right;
            VehiclesNameListGrid.VerticalAlignment = VerticalAlignment.Bottom;
            VehiclesNameListGrid.Background = (Brush)Converter.ConvertFromString("#FF716A6E");
            mainWindow.RegisterName("VehiclesNameList", VehiclesNameListGrid);
            VehiclesNameListGrid.Width = mainWindow.Footer.ActualWidth / 2;
            VehiclesNameListGrid.Height = 0;

            int index = 0;
            
            foreach (Vehicle vehicle in user.Vehicles)
            {
                VehiclesNameListGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60) });
                VehiclesNameListGrid.Height += 60;

                Grid grid = new();
                grid.Background = (Brush)Converter.ConvertFromString("#FF716A6E");
                grid.Margin = new Thickness(5);
                Grid.SetRow(grid, index);
                grid.Name = "Vehicle_" + index.ToString();
                //grid.Name = "Vehicle_" + vehicle.Id.ToString();
                grid.MouseLeave += VehicleListItemMouseLeave;
                grid.MouseEnter += VehicleListItemMouseEnter;
                grid.MouseLeftButtonDown += VehicleListItemClick;

                TextBlock textBlock = new();
                textBlock.Text = vehicle.Brand + " " + vehicle.Model;
                textBlock.FontSize = 16;
                textBlock.FontFamily = new FontFamily("Global User Interface");
                textBlock.FontWeight = FontWeights.Bold;
                textBlock.Foreground = (Brush)Converter.ConvertFromString("#EDEDED");
                textBlock.Background = Brushes.Transparent;

                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;

                grid.Children.Add(textBlock);

                VehiclesNameListGrid.Children.Add(grid);

                index++;
            }

            GenerateAnimation("Entry", VehiclesNameListGrid.Height);
            GenerateAnimation("Exit", VehiclesNameListGrid.Height);

            mainWindow.MainPanel.Children.Add(VehiclesNameListGrid);

            mainWindow.BeginStoryboard(EntryHeightAnimationStoryboard);
        }

        private void InitialAssignValue(MainWindow mw, User user, Config paramConfig)
        {
            mainWindow = mw;
            PUser = user;
            config = paramConfig;
        }

        private async void VehicleListItemClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string SenderName = ((Grid)sender).Name;
            PUser.ActiveCarIndex = Int32.Parse(SenderName.Substring(8));

            mainWindow.Footer_VehicleName.Text = ((TextBlock)((Grid)sender).Children[0]).Text;
            mainWindow.Footer_VehicleMillage.Text = PUser.Vehicles[PUser.ActiveCarIndex].CarMillage.ToString() + " km";

            mainWindow.BeginStoryboard(ExitHeightAnimationStoryboard);

            PUser.SerializeData();
            RefreshPage(config);

            await Task.Delay(500);

            mainWindow.MainPanel.Children.Remove((UIElement)mainWindow.FindName("VehiclesNameList"));
            if (mainWindow.FindName("VehiclesNameList") != null)
            {
                mainWindow.UnregisterName("VehiclesNameList");
            }
            if (mainWindow.FindName("VehiclesNameListExitAnimation") != null)
            {
                mainWindow.UnregisterName("VehiclesNameListExitAnimation");
            }
        }

        private void VehicleListItemMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Grid)sender).Background = (Brush)Converter.ConvertFromString("#FF82797E");
        }

        private void VehicleListItemMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Grid)sender).Background = (Brush)Converter.ConvertFromString("#FF716A6E");
        }
        
        private void GenerateAnimation(string animationType, double gridHeight)
        {
            double Form = 0, To = 0;
            Storyboard storyboard = new Storyboard();
            string StoryboardName = "";

            switch (animationType)
            {
                case "Entry":
                    storyboard = EntryHeightAnimationStoryboard;
                    Form = 0;
                    To = gridHeight;

                    break;

                case "Exit":
                    storyboard = ExitHeightAnimationStoryboard;
                    Form = gridHeight;
                    To = 0;

                    StoryboardName = "VehiclesNameListExitAnimation";
                    mainWindow.RegisterName(StoryboardName, storyboard);

                    break;
            }

            DoubleAnimation HeightAnimation = new DoubleAnimation();
            HeightAnimation.From = Form;
            HeightAnimation.To = To;
            HeightAnimation.BeginTime = new TimeSpan(0, 0, 0);
            HeightAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(250));

            Storyboard.SetTargetName(HeightAnimation, "VehiclesNameList");
            Storyboard.SetTargetProperty(HeightAnimation, new PropertyPath(Grid.HeightProperty));

            storyboard.Children.Clear();
            storyboard.Children.Add(HeightAnimation);
        }

        private void RefreshPage(Config paramConfig)
        { 
            switch (mainWindow.WhereAreYou)
            {
                case "HomePage":
                    new HomeContentGenerator().GeneratorHomeContent(mainWindow, PUser, config.MainPanel.HomePage);
                break;

                case "CostsPage":
                    new CostContentGenerator().CostGenerator(mainWindow, PUser, config.MainPanel.CostPage);
                break;

                case "RefuelingHistoryPage":
                    new RefuelingHistoryContentGenerator().GeneratorRefulingHistory(mainWindow, PUser, config.MainPanel.RefuelingHistoryPage);
                break;

                case "CalculatorPage":
                    new CalculatorContentGenerator().CalculatorGenerator(mainWindow, PUser, config);
                break;

                case "AddRefuelingPage":
                    new AddRefuelingPageGenerator().PageGenerator(mainWindow, PUser, config);
                break;

                case "DataStatsPage":
                    new Stats_DataContent().PageGenerator(mainWindow, PUser, config.MainPanel.StatsPage.StatsDataPage);
                break;
            }
        }
    }
}
