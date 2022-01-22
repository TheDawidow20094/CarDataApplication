using Car_Data_Application.Models;
using Car_Data_Application.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Car_Data_Application.Controllers
{
    class SetingsContentGenerator
    {
        private MainWindow mainWindow;
        private User PUser;
        private ComboBox LanguageComboBox = new ComboBox();
        private ComboBox MetricUnitComboBox = new ComboBox();
        private ComboBox CurrencyComboBox = new ComboBox();

        public void GenerateSetingContent(MainWindow mw, User user)
        {
            PUser = user;
            mainWindow = mw;
            mainWindow.AddButon.Visibility = Visibility.Hidden;
            new CarDataAppController().SetButtonColor("SetingPaneButton", mainWindow.SidePanel.Children);

            Grid Grid = new Grid();
            for (int i = 0; i < 7; i++) // 7 is number of rows
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(50);
                Grid.RowDefinitions.Add(row);
                Grid.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.VerticalAlignment = VerticalAlignment.Center;
            }

            TextBlock LanguageText = new TextBlock();
            LanguageText.Text = "Język:";
            Grid.SetRow(LanguageText, 0);
            Grid.Children.Add(LanguageText);

            LanguageComboBox.Height = 35;
            LanguageComboBox.Width = 100;
            LanguageComboBox.SelectedItem = user.UserLanguage;
            LanguageComboBox.Items.Add("Polski");
            LanguageComboBox.Items.Add("English");
            LanguageComboBox.SelectionChanged += HandleChangeLanguage;
            Grid.SetRow(LanguageComboBox, 1);
            Grid.Children.Add(LanguageComboBox);

            TextBlock MetricUnitText = new TextBlock();
            MetricUnitText.Text = "Jednostki metryczne:";
            Grid.SetRow(MetricUnitText, 2);
            Grid.Children.Add(MetricUnitText);

            MetricUnitComboBox.Height = 35;
            MetricUnitComboBox.Width = 100;
            MetricUnitComboBox.SelectedItem = user.MetricUnit;
            MetricUnitComboBox.Items.Add("Litrów/100km");
            MetricUnitComboBox.Items.Add("Mil/Galon");
            MetricUnitComboBox.SelectionChanged += HandleChangeMetricUnit;
            Grid.SetRow(MetricUnitComboBox, 3);
            Grid.Children.Add(MetricUnitComboBox);

            TextBlock CurrencyText = new TextBlock();
            CurrencyText.Text = "Waluta:";
            Grid.SetRow(CurrencyText, 4);
            Grid.Children.Add(CurrencyText);

            CurrencyComboBox.Height = 35;
            CurrencyComboBox.Width = 100;
            CurrencyComboBox.SelectedItem = user.Currency;
            CurrencyComboBox.Items.Add("PLN");
            CurrencyComboBox.Items.Add("EUR");
            CurrencyComboBox.Items.Add("USD");
            CurrencyComboBox.SelectionChanged += HandleChangeCurrency;
            Grid.SetRow(CurrencyComboBox, 5);
            Grid.Children.Add(CurrencyComboBox);

            Button ApplicationDevelopersButton = new Button();
            ApplicationDevelopersButton.Height = 35;
            ApplicationDevelopersButton.Width = 100;
            ApplicationDevelopersButton.Content = "Twórcy Aplikacji";
            ApplicationDevelopersButton.Click += ApplicationDevelopersButton_Click;
            Grid.SetRow(ApplicationDevelopersButton, 6);
            Grid.Children.Add(ApplicationDevelopersButton);


            mainWindow.ScrollViewerContent.Content = Grid;
        }

        private void HandleChangeCurrency(object sender, SelectionChangedEventArgs e)
        {
            PUser.Currency = CurrencyComboBox.SelectedItem.ToString();
            PUser.SerializeData();
        }

        private void HandleChangeMetricUnit(object sender, SelectionChangedEventArgs e)
        {
            PUser.MetricUnit = MetricUnitComboBox.SelectedItem.ToString();
            PUser.SerializeData();
        }

        private void HandleChangeLanguage(object sender, SelectionChangedEventArgs e)
        {
            PUser.UserLanguage = LanguageComboBox.SelectedItem.ToString();
            PUser.SerializeData();
        }

        private void ApplicationDevelopersButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dawid Kaczmarek : Jan Stark");
        }

    }
}
