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

namespace Car_Data_Application.Views
{
    public partial class MainWindow : Window
    {
        private BrushConverter Converter = new BrushConverter();
        User User = new User();
        public string WhereAreYou = string.Empty;
        Models.XML_Models.MainView Config;


        public MainWindow()
        {
            InitializeData();
            InitializeComponent();
            GenerateSidePanel();
            SetFooterData();
            new CarDataAppController().GoToHomePage(this, User);
        }

        private void GenerateSidePanel()
        {
            Config = ReadXML();
            Grid SidePanel = new Grid();
            SidePanel.SetValue(FrameworkElement.NameProperty, "SidePanel");
            SidePanel.Background = (Brush)Converter.ConvertFromString("#FF001A34");
            Grid.SetRow(SidePanel, 0);
            Grid.SetColumn(SidePanel, 0);

            int index = 0;
            foreach (Models.XML_Models.Button XmlButton in Config.SidePanel.Button)
            {
                if (bool.Parse(XmlButton.IsEnabled))
                {
                    RowDefinition row = new RowDefinition();
                    SidePanel.RowDefinitions.Add(row);

                    Button button = new Button();
                    button.Content = XmlButton.PL;
                    button.Name = XmlButton.Name;
                    button.Foreground = (Brush)Converter.ConvertFromString("#FFEDF5FD");
                    button.Background = Brushes.Transparent;
                    button.FontWeight = FontWeights.Bold;
                    button.Click += HandleSidePanelButtonClick;
                    button.MouseEnter += HandleSidePanelButtonEnter;
                    button.MouseLeave += HandleSidePanelButtonLeave;

                    Grid.SetRow(button, index);
                    SidePanel.Children.Add(button);
                    index++;
                }
            }
            this.MainGrid.Children.Add(SidePanel);
        }

        private void HandleSidePanelButtonLeave(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;

            if (!(WhereAreYou == button.Name))
            {
                button.Foreground = (Brush)Converter.ConvertFromString("#FFEDF5FD");
            }
        }

        private void HandleSidePanelButtonEnter(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.Foreground = (Brush)Converter.ConvertFromString("#FF001A34");
        }

        private void HandleSidePanelButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            switch (button.Name)
            {
                case "LoginPage":
                    new LoginWindow(this, User).ShowDialog();
                    break;

                case "LogoutPage":

                    break;

                case "HomePage":
                    new HomeContentGenerator().GeneratorHomeContent(this, User);
                    break;

                case "VehiclesPage":
                    new VehiclesContentGenerator().GeneratorVechicleList(this, User);
                    break;

                case "RefuelingHistoryPage":
                    new RefuelingHistoryContentGenerator().GeneratorRefulingHistory(this, User);
                    break;

                case "StatsPage":

                    break;

                case "CostsPage":
                    new CostContentGenerator().CostGenerator(this, User);
                    break;

                case "BackupPage":
                    break;

                case "CalculatorPage":
                    new CalculatorContentGenerator().CalculatorGenerator(this, User);
                    break;

                case "SettingsPage":
                    new SettingsContentGenerator().GenerateSetingContent(this, User);
                    break;
            }
        }

        private Models.XML_Models.MainView ReadXML()
        {
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(Models.XML_Models.MainView));
            FileStream XmlFileStream = new FileStream(@"../../../JSON_Files/Config.xml", FileMode.Open);
            Models.XML_Models.MainView Config = (Models.XML_Models.MainView)XmlSerializer.Deserialize(XmlFileStream);
            
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
            new GenerateSelectedCar().GeneratorCarSelectList(this, User);
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
                    new AddVehiclePageGenerator().PageGenerator(this, User);
                break;

                case "HomePage":
                    MessageBox.Show("HomePage");
                break;

                case "RefuelingHistoryPage":
                    MessageBox.Show("RefuelingPage");
                break;
            }
        }
    }
}
