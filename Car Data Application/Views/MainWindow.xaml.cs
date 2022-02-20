using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Car_Data_Application.Models;
using Car_Data_Application.Controllers;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Xml;
using Car_Data_Application.Models.XML_Models;
using System.Windows.Media.Animation;

namespace Car_Data_Application.Views
{
    public partial class MainWindow : Window
    {
        private LoginPanelGenerator LoginPanelGenerator = new();
        private HomeContentGenerator HomeContentGenerator = new();
        private VehiclesContentGenerator VehiclesContentGenerator = new();
        private RefuelingHistoryContentGenerator RefuelingHistoryContentGenerator = new();
        private CostContentGenerator CostContentGenerator = new();
        private CalculatorContentGenerator CalculatorContentGenerator = new();
        private SettingsContentGenerator SettingsContentGenerator = new();
        private GenerateSelectedCar GenerateSelectedCar = new();
        private AddRefuelingPageGenerator AddRefuelingPageGenerator = new();
        private AddCostPageGenerator AddCostPageGenerator = new();
        private AddVehiclePageGenerator AddVehiclePageGenerator = new();
        private BackupPanelGenerator BackupPanelGenerator = new();
        private Stats_DataContent Stats_DataContent = new();

        private BrushConverter Converter = new BrushConverter();
        public string WhereAreYou = string.Empty;
        private User User = new User();
        private Config config;
        private CarDataAppController carDataAppController;
        private Grid SidePanel;


        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
            SetFooterData();

            carDataAppController.GoToHomePage(this, User, config);
        }

        private void InitializeData()
        {
            carDataAppController = new CarDataAppController() { PUser = User, mainWindow = this, config = config };
            string JsonResultUser = File.ReadAllText(@"../../../JSON_Files/VehiclesTestJson.json", Encoding.UTF8);
            User = JsonConvert.DeserializeObject<User>(JsonResultUser);
            config = ReadXML();
            GenerateSidePanel();
        }

        private void GenerateSidePanel()
        {
            ScrollViewer SidePanelScrollViewer = new();
            SidePanelScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            Grid.SetRow(SidePanelScrollViewer, 0);
            Grid.SetColumn(SidePanelScrollViewer, 0);

            SidePanel = new Grid();
            if (this.FindName("SidePanel") != null)
            {
                this.UnregisterName("SidePanel");
            }
            this.RegisterName("SidePanel", SidePanel);
            SidePanel.Background = (Brush)Converter.ConvertFromString("#FF2A2729");

            SidePanelScrollViewer.Content = SidePanel;

            int index = 0;
            foreach (XMLButton XmlButton in config.SidePanel.XMLButton)
            {
                if (XmlButton.IsEnabled)
                {

                    Grid SidePanelButton = new Grid();
                    SidePanelButton.Name = XmlButton.Name;
                    SidePanelButton.Background = Brushes.Transparent;
                    SidePanelButton.MouseLeftButtonDown += HandleSidePanelButtonClick;
                    SidePanelButton.MouseEnter += HandleSidePanelButtonEnter;
                    SidePanelButton.MouseLeave += HandleSidePanelButtonLeave;

                    TextBlock SidePanelButtonContent = new TextBlock();
                    SidePanelButtonContent.Name = XmlButton.Name + "_TextBlock";
                    SidePanelButtonContent.Foreground = (Brush)Converter.ConvertFromString("#EDEDED");
                    SidePanelButtonContent.FontWeight = FontWeights.Bold;
                    SidePanelButtonContent.FontFamily = new FontFamily("Global User Interface");
                    SidePanelButtonContent.VerticalAlignment = VerticalAlignment.Center;
                    SidePanelButtonContent.HorizontalAlignment = HorizontalAlignment.Left;

                    RowDefinition rowDefinition = new RowDefinition();
                    if (XmlButton.IsSmallButton)
                    {
                        SidePanelButtonContent.Margin = new Thickness(70, 0, 0, 0);
                        SidePanelButtonContent.FontSize = 14;
                        rowDefinition.Height = new GridLength(0);
                        rowDefinition.Tag = "Statistics";
                    }
                    else
                    {
                        SidePanelButtonContent.Margin = new Thickness(50, 0, 0, 0);
                        SidePanelButtonContent.FontSize = 16;
                    }
                    SidePanel.RowDefinitions.Add(rowDefinition);


                    switch (User.UserLanguage)
                    {
                        case "PL":
                            SidePanelButtonContent.Text = XmlButton.PL;
                            break;

                        case "ENG":
                            SidePanelButtonContent.Text = XmlButton.ENG;
                            break;
                    }

                    SidePanelButton.Children.Add(SidePanelButtonContent);

                    Image SidePanelButtonIcon = new Image();
                    ImageSourceConverter source = new ImageSourceConverter();
                    SidePanelButtonIcon.SetValue(Image.SourceProperty, source.ConvertFromString("../../../Images/Icons/SidePanel/" + XmlButton.Icon));
                    SidePanelButtonIcon.Width = 22;
                    SidePanelButtonIcon.Height = 22;
                    SidePanelButtonIcon.HorizontalAlignment = HorizontalAlignment.Left;
                    SidePanelButtonIcon.VerticalAlignment = VerticalAlignment.Center;
                    if (XmlButton.IsSmallButton)
                    {
                        SidePanelButtonIcon.Margin = new Thickness(38, 0, 0, 0);
                    }
                    else
                    {
                        SidePanelButtonIcon.Margin = new Thickness(18, 0, 0, 0);
                    }

                    SidePanelButton.Children.Add(SidePanelButtonIcon);

                    Border SidePanelButtonBorder = new Border();
                    SidePanelButtonBorder.Name = XmlButton.Name;
                    SidePanelButtonBorder.Background = Brushes.Transparent;
                    SidePanelButtonBorder.HorizontalAlignment = HorizontalAlignment.Left;
                    SidePanelButtonBorder.Width = 6;
                    Grid.SetRow(SidePanelButtonBorder, index);

                    SidePanelButton.Children.Add(SidePanelButtonBorder);

                    Grid.SetRow(SidePanelButton, index);
                    SidePanel.Children.Add(SidePanelButton);
                    index++;
                }
            }
            this.MainGrid.Children.Add(SidePanelScrollViewer);
        }

        public void OpenPage(string pageName)
        {

            this.AddButtonList.Visibility = Visibility.Hidden;

            switch (pageName)
            {
                case "LoginPage":
                    AddButon.Visibility = Visibility.Hidden;
                    LoginPanelGenerator.PageGenerator(this, User, config);
                    break;

                case "LogoutPage":
                    AddButon.Visibility = Visibility.Hidden;
                    break;

                case "HomePage":
                    AddButon.Visibility = Visibility.Visible;
                    HomeContentGenerator.GeneratorHomeContent(this, User, config.MainPanel.HomePage);
                    break;

                case "VehiclesPage":
                    AddButon.Visibility = Visibility.Visible;
                    VehiclesContentGenerator.GeneratorVechicleList(this, User, config);
                    break;

                case "RefuelingHistoryPage":
                    AddButon.Visibility = Visibility.Visible;
                    RefuelingHistoryContentGenerator.GeneratorRefulingHistory(this, User, config.MainPanel.RefuelingHistoryPage);
                    break;

                case "StatsPage":
                    AddButon.Visibility = Visibility.Hidden;
                    
                    foreach (RowDefinition rowDefinition in SidePanel.RowDefinitions)
                    {
                        if ((rowDefinition.Tag == "Statistics") && (rowDefinition.ActualHeight == 0))
                        {
                            rowDefinition.Height = new GridLength(40);
                            SidePanel.Height = SidePanel.ActualHeight + 80;
                        }
                        else if ((rowDefinition.Tag == "Statistics") && (rowDefinition.ActualHeight == 40))
                        {
                            rowDefinition.Height = new GridLength(0);
                            SidePanel.Height = SidePanel.ActualHeight - 80;
                        }

                    }

                    this.WhereAreYou = "StatsPage";
                    carDataAppController.SetButtonColor(this.WhereAreYou, SidePanel);
                    break;

                case "DataStatsPage":
                    AddButon.Visibility = Visibility.Hidden;
                    Stats_DataContent.PageGenerator(this, User, config.MainPanel.StatsPage.StatsDataPage);
                    break;

                case "ChartsStatsPage":
                    AddButon.Visibility = Visibility.Hidden;
                    this.WhereAreYou = "ChartsStatsPage";
                    carDataAppController.SetButtonColor(this.WhereAreYou, SidePanel);
                    break;

                case "CostsPage":
                    AddButon.Visibility = Visibility.Visible;
                    CostContentGenerator.CostGenerator(this, User, config.MainPanel.CostPage);
                    break;

                case "BackupPage":
                    AddButon.Visibility = Visibility.Hidden;
                    BackupPanelGenerator.PanelGenerator(this, User, config.MainPanel.BackupPanel);
                    break;

                case "CalculatorPage":
                    AddButon.Visibility = Visibility.Hidden;
                    CalculatorContentGenerator.CalculatorGenerator(this, User, config);
                    break;

                case "SettingsPage":
                    AddButon.Visibility = Visibility.Hidden;
                    SettingsContentGenerator.GenerateSetingContent(this, User, config.MainPanel.SettingsPage);
                    break;
            }
        }

        private void HandleSidePanelButtonClick(object sender, RoutedEventArgs e)
        {
            OpenPage(((Grid)sender).Name);
        }

        private void HandleSidePanelButtonLeave(object sender, MouseEventArgs e)
        {
            Grid button = (Grid)sender;
            button.Background = Brushes.Transparent;
        }

        private void HandleSidePanelButtonEnter(object sender, MouseEventArgs e)
        {
            Grid button = (Grid)sender;
            button.Background = (Brush)Converter.ConvertFromString("#FF424041");
        }

        private Config ReadXML()
        {
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(Config));
            FileStream XmlFileStream = new FileStream(@"../../../JSON_Files/Config.xml", FileMode.Open);
            Config config = (Config)XmlSerializer.Deserialize(XmlFileStream);
            
            return config;
        }

        private void SetFooterData()
        {
            Footer_VehicleName.Text = User.Vehicles[User.ActiveCarIndex].Brand + " " + User.Vehicles[User.ActiveCarIndex].Model;
            Footer_VehicleMillage.Text = User.Vehicles[User.ActiveCarIndex].CarMillage + " km";
            UserName.Text = User.Login;
        }

        private async void ChangeActiveVehicleClick(object sender, RoutedEventArgs e)
        {
            Grid VehiclesNameList = (Grid)this.FindName("VehiclesNameList");
            if (VehiclesNameList == null)
            {
                GenerateSelectedCar.GeneratorCarSelectList(this, User, config);
            }
            else
            {
                this.BeginStoryboard((Storyboard)this.FindName("VehiclesNameListExitAnimation"));
                await Task.Delay(500);

                this.MainPanel.Children.Remove((UIElement)this.FindName("VehiclesNameList"));
                if (this.FindName("VehiclesNameList") != null)
                {
                    this.UnregisterName("VehiclesNameList");
                }
                if (this.FindName("VehiclesNameListExitAnimation") != null)
                {
                    this.UnregisterName("VehiclesNameListExitAnimation");
                }
            }
        }

        private void HandleActiveVehicleMouseEnter(object sender, MouseEventArgs e)
        {
            ((Grid)sender).Background = (Brush)Converter.ConvertFromString("#FF82797E");
        }

        private void HandleActiveVehicleMouseLeave(object sender, MouseEventArgs e)
        {
            ((Grid)sender).Background = Brushes.Transparent;
        }

        private void HandleAddButonMouseEnter(object sender, MouseEventArgs e)
        {
            AddButon.Background = (Brush)Converter.ConvertFromString("#FF4FE84F");
        }

        private void HandleAddButonMouseLeave(object sender, MouseEventArgs e)
        {
            AddButon.Background = (Brush)Converter.ConvertFromString("#FF38AE38");
        }

        private void HandleAddButtonClick(object sender, MouseButtonEventArgs e)
        {
            switch (WhereAreYou)
            {
                case "VehiclesPage":
                    AddVehiclePageGenerator.PageGenerator(this, User, config);
                break;

                case "HomePage":
                    GenerateAddButtonListItems(config.MainPanel.AddButonList);
                    if (this.AddButtonList.Visibility == Visibility.Hidden) { this.AddButtonList.Visibility = Visibility.Visible; }
                    else { this.AddButtonList.Visibility = Visibility.Hidden; }
                break;

                case "RefuelingHistoryPage":
                    AddRefuelingPageGenerator.PageGenerator(this, User, config);
                break;

                case "CostsPage":
                    AddCostPageGenerator.PageGenerator(this, User, config);
                    break;
            }
        }

        private void GenerateAddButtonListItems(AddButonList translation)
        {
            this.AddButtonList.Children.Clear();

            Button AddRefuelingButton = carDataAppController.GenerateButton(translation.AddRefueling, User.UserLanguage, 0, 0, fontSize: 13);
            AddRefuelingButton.Click += AddButtonListItemClick;
            this.AddButtonList.Children.Add(AddRefuelingButton);

            Button AddCostButton = carDataAppController.GenerateButton(translation.AddCost, User.UserLanguage, 1, 0, fontSize: 13);
            AddCostButton.Click += AddButtonListItemClick;
            this.AddButtonList.Children.Add(AddCostButton);

            Button AddVehicleButton = carDataAppController.GenerateButton(translation.AddVehicle, User.UserLanguage, 2, 0, fontSize: 13);
            AddVehicleButton.Click += AddButtonListItemClick;
            this.AddButtonList.Children.Add(AddVehicleButton);

        }

        private void AddButtonListItemClick(object sender, RoutedEventArgs e)
        {
            Button border = (Button)sender;
            switch (border.Name)
            {
                case "AddRefueling_Button":
                    AddRefuelingPageGenerator.PageGenerator(this, User, config);
                    this.AddButtonList.Visibility = Visibility.Hidden;
                    break;

                case "AddCost_Button":
                    AddCostPageGenerator.PageGenerator(this, User, config);
                    this.AddButtonList.Visibility = Visibility.Hidden;
                    break;

                case "AddVehicle_Button":
                    AddVehiclePageGenerator.PageGenerator(this, User, config);
                    this.AddButtonList.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void HandleWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Grid VehiclesNameList = (Grid)this.FindName("VehiclesNameList");
            if (VehiclesNameList != null)
            {
                VehiclesNameList.Width = this.Footer.ActualWidth / 2;
            }

            if ((this.Width <= 650) && (SidePanelColumn.ActualWidth == 210))
            {
                foreach (Grid Button in ((Grid)this.FindName("SidePanel")).Children)
                {
                    Button.Children[0].Visibility = Visibility.Hidden;
                }
                SidePanelColumn.Width = new GridLength(55);
            }
            else if ((this.Width > 650) && (SidePanelColumn.ActualWidth == 55))
            {
                foreach (Grid Button in ((Grid)this.FindName("SidePanel")).Children)
                {
                    Button.Children[0].Visibility = Visibility.Visible;
                }
                SidePanelColumn.Width = new GridLength(210);
            }
        }
    }
}
