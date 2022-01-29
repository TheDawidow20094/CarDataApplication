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
    class SettingsContentGenerator : CarDataAppController
    {
        private ComboBox LanguageComboBox = new ComboBox();
        private ComboBox MetricUnitComboBox = new ComboBox();
        private ComboBox CurrencyComboBox = new ComboBox();

        public void GenerateSetingContent(MainWindow mw, User user)
        {
            InitialAssignValue(mw, user);
            
            Grid Grid = new Grid();
            Grid.Children.Add(GenerateSettingContentBorder(PUser));

            mainWindow.ScrollViewerContent.Content = Grid;
        }

        private Border GenerateSettingContentBorder(User user)
        {
            Border SetingContentBorder = new Border();
            SetBorderProps(ref SetingContentBorder, 0);

            Grid VehicleNameGrid = new Grid();
            SetingContentBorder.Padding = new Thickness(20);
            SetingContentBorder.Child = VehicleNameGrid;
            for (int i = 0; i < 7; i++) // 7 is number of rows
            {
                RowDefinition VehicleNameGridRow = new RowDefinition();
                VehicleNameGrid.RowDefinitions.Add(VehicleNameGridRow);
                VehicleNameGrid.HorizontalAlignment = HorizontalAlignment.Center;
                VehicleNameGrid.VerticalAlignment = VerticalAlignment.Center;
            }

            VehicleNameGrid.Children.Add(GenerateTextBlock("Język:", 0));

            LanguageComboBox.Height = 35;
            LanguageComboBox.Width = 100;
            LanguageComboBox.Items.Add("Polski");
            LanguageComboBox.Items.Add("English");

            switch (user.UserLanguage)
            {
                case "PL":
                    LanguageComboBox.SelectedItem = "Polski";
                    break;

                case "ENG":
                    LanguageComboBox.SelectedItem = "English";
                    break;
            }

            LanguageComboBox.SelectionChanged += HandleChangeLanguage;
            Grid.SetRow(LanguageComboBox, 1);
            VehicleNameGrid.Children.Add(LanguageComboBox);

            VehicleNameGrid.Children.Add(GenerateTextBlock("Jednostki metryczne:", 2));

            MetricUnitComboBox.Height = 35;
            MetricUnitComboBox.Width = 100;
            MetricUnitComboBox.SelectedItem = user.MetricUnit;
            MetricUnitComboBox.Items.Add("Litrów/100km");
            MetricUnitComboBox.Items.Add("Mil/Galon");
            MetricUnitComboBox.SelectionChanged += HandleChangeMetricUnit;
            Grid.SetRow(MetricUnitComboBox, 3);
            VehicleNameGrid.Children.Add(MetricUnitComboBox);

            VehicleNameGrid.Children.Add(GenerateTextBlock("Waluta:", 4));

            CurrencyComboBox.Height = 35;
            CurrencyComboBox.Width = 100;
            CurrencyComboBox.SelectedItem = user.Currency;
            CurrencyComboBox.Items.Add("PLN");
            CurrencyComboBox.Items.Add("EUR");
            CurrencyComboBox.Items.Add("USD");
            CurrencyComboBox.SelectionChanged += HandleChangeCurrency;
            Grid.SetRow(CurrencyComboBox, 5);
            VehicleNameGrid.Children.Add(CurrencyComboBox);

            return SetingContentBorder;
        }

        private void InitialAssignValue(MainWindow mw, User user)
        {
            PUser = user;
            mainWindow = mw;
            mainWindow.AddButon.Visibility = Visibility.Hidden;
            mainWindow.WhereAreYou = "SettingsPage";
            SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.MainGrid.Children[2]).Children);
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
            switch (LanguageComboBox.SelectedItem.ToString())
            {
                case "Polski":
                    PUser.UserLanguage = "PL";
                    break;

                case "English":
                    PUser.UserLanguage = "ENG";
                    break;
            }
            PUser.SerializeData();
        }

        private void ApplicationDevelopersButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dawid Kaczmarek : Jan Stark");
        }

        public void SetBorderProps(ref Border border, int row)
        {
            Brush BackgroundBrushh = (Brush)Converter.ConvertFromString("#FF001A34");
            border.Background = BackgroundBrushh;

            border.BorderThickness = new Thickness(5);
            border.BorderBrush = (Brush)Converter.ConvertFrom("#FF407BB6");
            border.CornerRadius = new CornerRadius(30);

            border.Margin = new Thickness(15, 5, 15, 5);
            border.Padding = new Thickness(0, 0, 35, 0);
            Grid.SetRow(border, row);
        }

        private TextBlock GenerateTextBlock(string text, int row)
        {
            TextBlock TextBlockName = new TextBlock();
            TextBlockName.Foreground = (Brush)Converter.ConvertFromString("#FFEDF5FD");
            TextBlockName.FontFamily = new FontFamily("Arial Black");
            TextBlockName.FontWeight = FontWeights.Bold;
            TextBlockName.Text = text;
            TextBlockName.HorizontalAlignment = HorizontalAlignment.Center;
            TextBlockName.VerticalAlignment = VerticalAlignment.Center;
            TextBlockName.Margin = new Thickness(0, 15, 0, 15);
            Grid.SetRow(TextBlockName, row);

            return TextBlockName;
        }

        private Button ApplyButton()
        {
            Button ApplyButton = new Button();

            return ApplyButton;
        }

    }
}
