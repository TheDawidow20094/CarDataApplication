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
        private BrushConverter Converter = new BrushConverter();
        User User = new User();
        public string WhereAreYou = string.Empty;
        MainGrid Config;


        public MainWindow()
        {
            InitializeData();
            InitializeComponent();
            GenerateSidePanel();
            SetFooterData();
            new CarDataAppController().GoToHomePage(this, User, Config);
        }

        private void GenerateSidePanel()
        {
            Config = ReadXML();

            Grid SidePanel = new Grid();
            SidePanel.SetValue(FrameworkElement.NameProperty, "SidePanel");
            SidePanel.Background = (Brush)Converter.ConvertFromString("#FF2A2729");
            Grid.SetRow(SidePanel, 0);
            Grid.SetColumn(SidePanel, 0);

            int index = 0;
            foreach (XMLButton XmlButton in Config.SidePanel.XMLButton)
            {
                if (XmlButton.IsEnabled)
                {
                    SidePanel.RowDefinitions.Add(new RowDefinition());

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
                    SidePanelButtonContent.FontSize = 16;

                    SidePanelButtonContent.VerticalAlignment = VerticalAlignment.Center;
                    SidePanelButtonContent.HorizontalAlignment = HorizontalAlignment.Left;
                    SidePanelButtonContent.Margin = new Thickness(50, 0, 0, 0);

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
                    SidePanelButtonIcon.Margin = new Thickness(18, 0, 0, 0);

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
            this.MainGrid.Children.Add(SidePanel);
        }

        private void HandleSidePanelButtonClick(object sender, RoutedEventArgs e)
        {
            Grid button = (Grid)sender;

            AddButonList.Visibility = Visibility.Hidden;

            switch (button.Name)
            {
                case "MyAccountPage":
                    AddButon.Visibility = Visibility.Hidden;
                    new LoginWindow(this, User, Config).ShowDialog();
                    break;

                case "LogoutPage":
                    AddButon.Visibility = Visibility.Hidden;
                    break;

                case "HomePage":
                    AddButon.Visibility = Visibility.Visible;
                    new HomeContentGenerator().GeneratorHomeContent(this, User, Config.MainPanel.HomePage);
                    break;

                case "VehiclesPage":
                    AddButon.Visibility = Visibility.Visible;
                    new VehiclesContentGenerator().GeneratorVechicleList(this, User, Config);
                    break;

                case "RefuelingHistoryPage":
                    AddButon.Visibility = Visibility.Visible;
                    new RefuelingHistoryContentGenerator().GeneratorRefulingHistory(this, User);
                    break;

                case "StatsPage":
                    AddButon.Visibility = Visibility.Hidden;
                    break;

                case "CostsPage":
                    AddButon.Visibility = Visibility.Visible;
                    new CostContentGenerator().CostGenerator(this, User);
                    break;

                case "BackupPage":
                    AddButon.Visibility = Visibility.Hidden;
                    break;

                case "CalculatorPage":
                    AddButon.Visibility = Visibility.Hidden;
                    new CalculatorContentGenerator().CalculatorGenerator(this, User, Config);
                    break;

                case "SettingsPage":
                    AddButon.Visibility = Visibility.Hidden;
                    new SettingsContentGenerator().GenerateSetingContent(this, User, Config);
                    break;
            }
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

        private MainGrid ReadXML()
        {
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(MainGrid));
            FileStream XmlFileStream = new FileStream(@"../../../JSON_Files/Config.xml", FileMode.Open);
            MainGrid Config = (MainGrid)XmlSerializer.Deserialize(XmlFileStream);
            
            return Config;
        }

        private void InitializeData()
        {
            string JsonResultUser = File.ReadAllText(@"../../../JSON_Files/VehiclesTestJson.json", Encoding.UTF8);
            User = JsonConvert.DeserializeObject<User>(JsonResultUser);
        }

        private void SetFooterData()
        {
            CarNameButton.Content = User.Vehicles[User.ActiveCarIndex].Brand + " " + User.Vehicles[User.ActiveCarIndex].Model;
            UserName.Text = User.Login;
        }

        private void ChangeActiveCarClick(object sender, RoutedEventArgs e)
        {
            new GenerateSelectedCar().GeneratorCarSelectList(this, User, Config);
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
                    new AddVehiclePageGenerator().PageGenerator(this, User, Config);
                break;

                case "HomePage":
                    TranslateControlersValue(Config.MainPanel.AddButonList);
                    if (AddButonList.Visibility == Visibility.Hidden) { AddButonList.Visibility = Visibility.Visible; }
                    else { AddButonList.Visibility = Visibility.Hidden; }
                break;

                case "RefuelingHistoryPage":
                    new AddRefuelingPageGenerator().PageGenerator(this, User, Config);
                break;
            }
        }

        private void HandleAddButtonListMouseEnter(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.Background = (Brush)Converter.ConvertFromString("#FF5BA05B");
        }

        private void HandleAddButtonListMouseLeave(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.Background = (Brush)Converter.ConvertFromString("#FF5C7B9B");
        }

        private void HandleAddButtonControllerClick(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            switch (border.Name)
            {
                case "AddRefueling":
                    new AddRefuelingPageGenerator().PageGenerator(this, User, Config);
                    AddButonList.Visibility = Visibility.Hidden;
                    break;

                case "AddService":
                    break;

                case "AddVehicle":
                    new AddVehiclePageGenerator().PageGenerator(this, User, Config);
                    AddButonList.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void TranslateControlersValue(AddButonList translation)
        {
            switch (User.UserLanguage)
            {
                case "PL":
                    AddRefuelingTextBlock.Text = translation.AddRefueling.PL;
                    AddServiceTextBlock.Text = translation.AddCost.PL;
                    AddVehicleTextBlock.Text = translation.AddVehicle.PL;
                    break;

                case "ENG":
                    AddRefuelingTextBlock.Text = translation.AddRefueling.ENG;
                    AddServiceTextBlock.Text = translation.AddCost.ENG;
                    AddVehicleTextBlock.Text = translation.AddVehicle.ENG;
                    break;
            }
        }

        
        private void HandleWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //size changes
            if ((this.Width <= 550) && (SidePanelColumn.ActualWidth == 210))
            {
                foreach (Grid Button in ((Grid)MainGrid.Children[3]).Children)
                {
                    Button.Children[0].Visibility = Visibility.Hidden;
                }
                SidePanelColumn.Width = new GridLength(55);
            }
            else if ((this.Width > 550) && (SidePanelColumn.ActualWidth == 55))
            {
                foreach (Grid Button in ((Grid)MainGrid.Children[3]).Children)
                {
                    Button.Children[0].Visibility = Visibility.Visible;
                }
                SidePanelColumn.Width = new GridLength(210);
            }
        }
    }
}
