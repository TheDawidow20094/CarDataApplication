using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Car_Data_Application.Controllers
{
    class SettingsContentGenerator : CarDataAppController
    {
        private ToggleButton LanguagePLToggleButton;
        private ToggleButton LanguageENGToggleButton;

        private ToggleButton UnitsMetricToggleButton;
        private ToggleButton UnitsImperialToggleButton;

        private ToggleButton CurrencyPLNToggleButton;
        private ToggleButton CurrencyEURToggleButton;
        private ToggleButton CurrencyUSDToggleButton;

        private string SelectedLanguage;
        private string SelectedUnits;
        private string SelectedCurrency;

        public void GenerateSetingContent(MainWindow mw, User user, SettingsPage translation)
        {
            InitialAssignValue(mw, user);
            
            Grid MainGrid = new Grid();

            for (int i = 0; i < 4; i++) // 4 is number of rows
            {
                MainGrid.RowDefinitions.Add(new RowDefinition());
            }

            MainGrid.Children.Add(GenerateChangeLanguageContentBorder(PUser, translation));
            MainGrid.Children.Add(GenerateChangeUnitsContentBorder(PUser, translation));
            MainGrid.Children.Add(GenerateChangeCurrencyContentBorder(PUser, translation));
            MainGrid.Children.Add(GenerateSettingsButton(translation));

            CheckUserSettings();

            mainWindow.ScrollViewerContent.Content = MainGrid;
        }

        private void InitialAssignValue(MainWindow mw, User user)
        {
            PUser = user;
            mainWindow = mw;
            mainWindow.WhereAreYou = "SettingsPage";
            SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.FindName("SidePanel")));
        }

        private void CheckUserSettings()
        {
            switch (PUser.UserLanguage)
            {
                case "PL":
                    LanguagePLToggleButton.IsChecked = true;
                    break;

                case "ENG":
                    LanguageENGToggleButton.IsChecked = true;
                    break;
            }

            switch (PUser.UnitsType)
            {
                case "Metric":
                    UnitsMetricToggleButton.IsChecked = true;
                    break;

                case "Imperial":
                    UnitsImperialToggleButton.IsChecked = true;
                    break;
            }

            switch (PUser.Currency)
            {
                case "PLN":
                    CurrencyPLNToggleButton.IsChecked = true;
                    break;

                case "EUR":
                    CurrencyEURToggleButton.IsChecked = true;
                    break;

                case "USD":
                    CurrencyUSDToggleButton.IsChecked = true;
                    break;
            }
        }

        private Grid GenerateChangeLanguageContentBorder(User user, SettingsPage translation)
        {
            Grid ChangeLanguageGrid = new Grid();
            SetGridProps(ref ChangeLanguageGrid, 0);

            for (int i = 0; i < 2; i++)
            {
                ChangeLanguageGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            ChangeLanguageGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            ChangeLanguageGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(80) });

            ChangeLanguageGrid.Children.Add(GenerateTextBlock(translation.Language, PUser.UserLanguage, 0, 0, isTitle: true, horizontalAlignment: HorizontalAlignment.Center));

            LanguagePLToggleButton = GenerateToggleButtonWithHandlers(translation.LanguagePL, PUser.UserLanguage, 1, 0);
            LanguageENGToggleButton = GenerateToggleButtonWithHandlers(translation.LanguageENG, PUser.UserLanguage, 1, 1);

            ChangeLanguageGrid.Children.Add(LanguagePLToggleButton);
            ChangeLanguageGrid.Children.Add(LanguageENGToggleButton);

            return ChangeLanguageGrid;
        }

        private Grid GenerateChangeUnitsContentBorder(User user, SettingsPage translation)
        {
            Grid ChangeUnitGrid = new Grid();
            SetGridProps(ref ChangeUnitGrid, 1);

            for (int i = 0; i < 2; i++)
            {
                ChangeUnitGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            ChangeUnitGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            ChangeUnitGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(80) });

            ChangeUnitGrid.Children.Add(GenerateTextBlock(translation.UnitsOfMeasure, PUser.UserLanguage, 0, 0, isTitle: true, horizontalAlignment: HorizontalAlignment.Center));

            UnitsMetricToggleButton = GenerateToggleButtonWithHandlers(translation.UnitsMetric, PUser.UserLanguage, 1, 0);
            UnitsImperialToggleButton = GenerateToggleButtonWithHandlers(translation.UnitsImperial, PUser.UserLanguage, 1, 1);

            ChangeUnitGrid.Children.Add(UnitsMetricToggleButton);
            ChangeUnitGrid.Children.Add(UnitsImperialToggleButton);

            return ChangeUnitGrid;
        }

        private Grid GenerateChangeCurrencyContentBorder(User user, SettingsPage translation)
        {
            Grid ChangeCurrencyGrid = new Grid();
            SetGridProps(ref ChangeCurrencyGrid, 2);

            for (int i = 0; i < 3; i++)
            {
                ChangeCurrencyGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            ChangeCurrencyGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            ChangeCurrencyGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(80) });
            TextBlock textBlock;

            textBlock = GenerateTextBlock(translation.Currency, PUser.UserLanguage, 0, 0, isTitle: true, horizontalAlignment: HorizontalAlignment.Center);
            Grid.SetColumnSpan(textBlock, 3);
            ChangeCurrencyGrid.Children.Add(textBlock);

            CurrencyPLNToggleButton = GenerateToggleButtonWithHandlers(translation.CurrencyPLN, PUser.UserLanguage, 1, 0);
            CurrencyEURToggleButton = GenerateToggleButtonWithHandlers(translation.CurrencyEUR, PUser.UserLanguage, 1, 1);
            CurrencyUSDToggleButton = GenerateToggleButtonWithHandlers(translation.CurrencyUSD, PUser.UserLanguage, 1, 2);

            ChangeCurrencyGrid.Children.Add(CurrencyPLNToggleButton);
            ChangeCurrencyGrid.Children.Add(CurrencyEURToggleButton);
            ChangeCurrencyGrid.Children.Add(CurrencyUSDToggleButton);

            return ChangeCurrencyGrid;
        }

        private Button GenerateSettingsButton(SettingsPage translation)
        {
            Button ApplySettingsButton = GenerateButton(translation.ApplyButton, PUser.UserLanguage, 3, 0, DarkTextColor);
            ApplySettingsButton.Background = (Brush)Converter.ConvertFromString("#FF93D68A");
            ApplySettingsButton.Click += HandleApplySettingsButtonClick;

            return ApplySettingsButton;
        }

        private ToggleButton GenerateToggleButtonWithHandlers(Translation text, string language, int row, int column)
        {
            ToggleButton toggleButton = GenerateToggleButton(text, language, row, column);

            toggleButton.Unchecked += SettingsToggleButtonUnchecked;
            toggleButton.Checked += SettingsToggleButtonChecked;

            return toggleButton;
        }

        private void SettingsToggleButtonChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = (ToggleButton)sender;
            toggleButton.Foreground = (Brush)Converter.ConvertFromString(DarkTextColor);

            switch (toggleButton.Name)
            {
                case "Polish_ToggleButton":
                    LanguageENGToggleButton.IsChecked = false;
                    SelectedLanguage = "PL";
                    break;

                case "English_ToggleButton":
                    LanguagePLToggleButton.IsChecked = false;
                    SelectedLanguage = "ENG";
                    break;

                    //--------------------------------------------------------------------------

                case "Metric_ToggleButton":
                    UnitsImperialToggleButton.IsChecked = false;
                    SelectedUnits = "Metric";
                    break;

                case "Imperial_ToggleButton":
                    UnitsMetricToggleButton.IsChecked = false;
                    SelectedUnits = "Imperial";
                    break;

                    //--------------------------------------------------------------------------

                case "PLN_ToggleButton":
                    CurrencyEURToggleButton.IsChecked = false;
                    CurrencyUSDToggleButton.IsChecked = false;
                    SelectedCurrency = CurrencyPLNToggleButton.Content.ToString();
                    break;

                case "EUR_ToggleButton":
                    CurrencyPLNToggleButton.IsChecked = false;
                    CurrencyUSDToggleButton.IsChecked = false;
                    SelectedCurrency = CurrencyEURToggleButton.Content.ToString();
                    break;

                case "USD_ToggleButton":
                    CurrencyPLNToggleButton.IsChecked = false;
                    CurrencyEURToggleButton.IsChecked = false;
                    SelectedCurrency = CurrencyUSDToggleButton.Content.ToString();
                    break;

            }
        }

        private void SettingsToggleButtonUnchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = (ToggleButton)sender;
            toggleButton.Foreground = (Brush)Converter.ConvertFromString(LightTextColor);
        }

        private void HandleApplySettingsButtonClick(object sender, RoutedEventArgs e)
        {
            if (SelectedLanguage != null)
            {
                PUser.UserLanguage = SelectedLanguage;
            }
            if (SelectedUnits != null)
            {
                PUser.UnitsType = SelectedUnits;
            }
            if (SelectedCurrency != null)
            {
                PUser.Currency = SelectedCurrency;
            }

            PUser.SerializeData();

            MainWindow RefreshApp = new MainWindow();
            RefreshApp.Show();
            mainWindow.Close();
        }
    }
}
