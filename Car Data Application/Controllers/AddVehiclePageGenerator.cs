using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Car_Data_Application.Controllers
{
    class AddVehiclePageGenerator : CarDataAppController
    {
        public void PageGenerator(MainWindow mw, User user, Config paramConfig)
        {
            InitialAssignValue(mw, user, paramConfig);

            Grid MainGrid = new Grid();

            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50)});
            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(100) });
            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(160) });
            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(150) });
            MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(170) });

            MainGrid.Children.Add(AddingTitle(config.MainPanel.AddVehiclePage));
            MainGrid.Children.Add(AddingVehiclePrimaryDataContent(config.MainPanel.AddVehiclePage));
            MainGrid.Children.Add(AddingPrimaryInfoContent(config.MainPanel.AddVehiclePage));
            MainGrid.Children.Add(AddingFuelTankInfoContent(config.MainPanel.AddVehiclePage));
            MainGrid.Children.Add(AddingCyclicalCostContent(config.MainPanel.AddVehiclePage));


            mainWindow.ScrollViewerContent.Content = MainGrid;

        }

        private void InitialAssignValue(MainWindow mw, User user, Config paramConfig)
        {
            config = paramConfig;
            mainWindow = mw;
            PUser = user;
            mainWindow.AddButon.Visibility = Visibility.Hidden;
            SetButtonColor("VehiclesPage", ((Grid)mainWindow.MainGrid.Children[3]));
        }

        private Grid AddingTitle(AddVehiclePage translation)
        {
            Grid TitleGrid = new Grid();
            TitleGrid.Children.Add(GenerateTextBlock(PUser.UserLanguage == "PL" ? translation.Title.PL : translation.Title.ENG , 0, 0, horizontalAlignment: HorizontalAlignment.Center, verticalAlignment: VerticalAlignment.Center, isTitle: true));

            return TitleGrid;
        }

        private Grid AddingVehiclePrimaryDataContent(AddVehiclePage translation)
        {
            Grid VehiclePrimaryDataGrid = new();
            VehiclePrimaryDataGrid.Margin = new Thickness(0, 5, 0, 5);
            Grid.SetRow(VehiclePrimaryDataGrid, 1);

            for (int i = 0; i < 2; i++) // 2 number of column
            {
                VehiclePrimaryDataGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            Grid AddImageGrid = new Grid();

            Rectangle AddImageRectangle = new();
            AddImageRectangle.Margin = new Thickness(0, 0, 15, 0);
            AddImageRectangle.Stroke = (Brush)Converter.ConvertFromString("#FF6D90B4");
            AddImageRectangle.StrokeThickness = 4;
            AddImageRectangle.StrokeDashArray = new DoubleCollection() { 4 };

            TextBlock AddPictureText = new();
            AddPictureText.Text = "+";
            AddPictureText.HorizontalAlignment = HorizontalAlignment.Center;
            AddPictureText.VerticalAlignment = VerticalAlignment.Center;
            AddPictureText.FontSize = 70;
            AddPictureText.FontWeight = FontWeights.UltraBold;
            AddPictureText.Foreground = (Brush)Converter.ConvertFromString("#FF6D90B4");


            AddImageGrid.Children.Add(AddImageRectangle);
            AddImageGrid.Children.Add(AddPictureText);

            Grid.SetColumn(AddImageGrid, 1);

            Grid VehicleNameGrid = new Grid();
            SetGridProps(ref VehicleNameGrid, 1);
            Grid.SetColumn(VehicleNameGrid, 0);

            for (int i = 0; i < 2; i++)
            {
                VehicleNameGrid.RowDefinitions.Add(new RowDefinition());
                VehicleNameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            switch (PUser.UserLanguage)
            {
                case "PL":
                    VehicleNameGrid.Children.Add(GenerateTextBlock(translation.Brand.PL, 0, 0, LightTextColor, HorizontalAlignment.Right));
                    VehicleNameGrid.Children.Add(GenerateTextBlock(translation.Model.PL, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    break;
                case "ENG":
                    VehicleNameGrid.Children.Add(GenerateTextBlock(translation.Brand.ENG, 0, 0, LightTextColor, HorizontalAlignment.Right));
                    VehicleNameGrid.Children.Add(GenerateTextBlock(translation.Model.ENG, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    break;
            }

            VehicleNameGrid.Children.Add(GenerateTextBox(translation.Brand.ENG.TrimEnd(':'), 0, 1, smallersize: true, horizontalAlignment: HorizontalAlignment.Left));

            VehicleNameGrid.Children.Add(GenerateTextBox(translation.Model.ENG.TrimEnd(':'), 1, 1, smallersize: true, horizontalAlignment: HorizontalAlignment.Left));

            VehiclePrimaryDataGrid.Children.Add(VehicleNameGrid);
            VehiclePrimaryDataGrid.Children.Add(AddImageGrid);

            return VehiclePrimaryDataGrid;
        }

        private Grid AddingPrimaryInfoContent(AddVehiclePage translation)
        {
            Grid PrimarmaryInfoGrid = new Grid();
            SetGridProps(ref PrimarmaryInfoGrid, 2);

            for (int i = 0; i < 2; i++) // 2 number of columns
            {
                PrimarmaryInfoGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 4; i++) // 4 number of rows
            {
                PrimarmaryInfoGrid.RowDefinitions.Add(new RowDefinition());
            }

            switch (PUser.UserLanguage)
            {
                case "PL":
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.YearOfManufacture.PL, 0, 0, LightTextColor, HorizontalAlignment.Right, VerticalAlignment.Center));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.VIN.PL, 1, 0, LightTextColor, HorizontalAlignment.Right, VerticalAlignment.Center));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Plates.PL, 2, 0, LightTextColor, HorizontalAlignment.Right, VerticalAlignment.Center));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.VehicleMillage.PL, 3, 0, LightTextColor, HorizontalAlignment.Right, VerticalAlignment.Center));
                    break;
                case "ENG":
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.YearOfManufacture.ENG, 0, 0, LightTextColor, HorizontalAlignment.Right));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.VIN.ENG, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Plates.ENG, 2, 0, LightTextColor, HorizontalAlignment.Right));
                    PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.VehicleMillage.ENG, 3, 0, LightTextColor, HorizontalAlignment.Right));
                    break;
            }

            PrimarmaryInfoGrid.Children.Add(GenerateTextBox(translation.YearOfManufacture.ENG.TrimEnd(':'), 0, 1, horizontalAlignment: HorizontalAlignment.Left));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBox(translation.VIN.ENG.TrimEnd(':'), 1, 1, horizontalAlignment: HorizontalAlignment.Left));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBox(translation.Plates.ENG.TrimEnd(':'), 2, 1, horizontalAlignment: HorizontalAlignment.Left));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBox(translation.VehicleMillage.ENG.TrimEnd(':'), 3, 1, horizontalAlignment: HorizontalAlignment.Left));

            return PrimarmaryInfoGrid;
        }

        private Grid AddingFuelTankInfoContent(AddVehiclePage translation)
        {
            Grid FuelInfoGrid = new Grid();
            SetGridProps(ref FuelInfoGrid, 3);

            for (int i = 0; i < 3; i++) // 3 number of columns
            {
                FuelInfoGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            FuelInfoGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(80)});
            FuelInfoGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });

            switch (PUser.UserLanguage)
            {
                case "PL":
                    FuelInfoGrid.Children.Add(GenerateToggleButton(translation.GasolineTank, "PL", 0, 0));
                    FuelInfoGrid.Children.Add(GenerateToggleButton(translation.LPGTank, "PL", 0, 1));
                    FuelInfoGrid.Children.Add(GenerateToggleButton(translation.DieselTank, "PL", 0, 2));
                    break;
                case "ENG":
                    FuelInfoGrid.Children.Add(GenerateToggleButton(translation.GasolineTank, "ENG", 0, 0));
                    FuelInfoGrid.Children.Add(GenerateToggleButton(translation.LPGTank, "ENG", 0, 1));
                    FuelInfoGrid.Children.Add(GenerateToggleButton(translation.DieselTank, "ENG", 0, 2));
                    break;
            }

            FuelInfoGrid.Children.Add(GenerateTextBox("GasolineTank", 1, 0, horizontalAlignment: HorizontalAlignment.Center, visibility: Visibility.Hidden));
            FuelInfoGrid.Children.Add(GenerateTextBox("LPGTank", 1, 1, horizontalAlignment: HorizontalAlignment.Center, visibility: Visibility.Hidden));
            FuelInfoGrid.Children.Add(GenerateTextBox("DieselTank", 1, 2, horizontalAlignment: HorizontalAlignment.Center, visibility: Visibility.Hidden));


            return FuelInfoGrid;
        }

        private Grid AddingCyclicalCostContent(AddVehiclePage translation)
        {
            Grid CyclicalCostGrid = new Grid();
            SetGridProps(ref CyclicalCostGrid, 4);


            for (int i = 0; i < 4; i++) // numbers of column and rows
            {
                CyclicalCostGrid.ColumnDefinitions.Add(new ColumnDefinition());
                CyclicalCostGrid.RowDefinitions.Add(new RowDefinition());
            }

            switch (PUser.UserLanguage)
            {
                case "PL":
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceTitle.PL, 0, 0, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceStartDate.PL, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceEndDate.PL, 2, 0, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsurancePrice.PL, 3, 0, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionTitle.PL, 0, 2, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionStartDate.PL, 1, 2, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionEndDate.PL, 2, 2, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionEndDate.PL, 3, 2, LightTextColor, HorizontalAlignment.Right));
                    break;
                case "ENG":
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceTitle.ENG, 0, 0, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceStartDate.ENG, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceEndDate.ENG, 2, 0, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsurancePrice.ENG, 3, 0, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionTitle.ENG, 0, 2, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionStartDate.ENG, 1, 2, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionEndDate.ENG, 2, 2, LightTextColor, HorizontalAlignment.Right));
                    CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionEndDate.ENG, 3, 2, LightTextColor, HorizontalAlignment.Right));
                    break;
            }

            CyclicalCostGrid.Children.Add(GenerateTextBox("InsuranceStartDate", 1, 1, horizontalAlignment: HorizontalAlignment.Left));

            CyclicalCostGrid.Children.Add(GenerateTextBox("InsuranceEndDate", 2, 1, horizontalAlignment: HorizontalAlignment.Left));

            CyclicalCostGrid.Children.Add(GenerateTextBox("InsurancePrice", 3, 1, horizontalAlignment: HorizontalAlignment.Left));




            CyclicalCostGrid.Children.Add(GenerateTextBox("InspectionStartDate", 1, 3, horizontalAlignment: HorizontalAlignment.Left));

            CyclicalCostGrid.Children.Add(GenerateTextBox("InspectionEndDate", 2, 3, horizontalAlignment: HorizontalAlignment.Left));

            CyclicalCostGrid.Children.Add(GenerateTextBox("InspectionPrice", 3, 3, horizontalAlignment: HorizontalAlignment.Left));

            return CyclicalCostGrid;
        }

        private void add_photo_button_Click()
        {
            var photo = new BitmapImage();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            Uri fileUri = new Uri(openFileDialog.FileName);
            photo = new BitmapImage(fileUri);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(photo));
            using (FileStream filestream = new FileStream(@"..\..\..\Images\", FileMode.Create))
            {
                encoder.Save(filestream);
                filestream.Close();
            }

        }

        public ToggleButton GenerateToggleButton(Translation text, string language, int row, int column)
        {
            ToggleButton toggleButton = new ToggleButton();
            switch (language)
            {
                case "PL":
                    toggleButton.Content = text.PL;
                    break;

                case "ENG":
                    toggleButton.Content = text.ENG;
                    break;
            }
            toggleButton.FontFamily = new FontFamily("Global User Interface");
            toggleButton.FontSize = 18;
            toggleButton.FontWeight = FontWeights.Bold;

            toggleButton.Height = 45;
            toggleButton.Width = 140;
            toggleButton.HorizontalAlignment = HorizontalAlignment.Center;
            toggleButton.VerticalAlignment = VerticalAlignment.Center;
            toggleButton.Margin = new Thickness(10);
            toggleButton.BorderThickness = new Thickness(0);

            toggleButton.Foreground = (Brush)Converter.ConvertFromString(LightTextColor);
            toggleButton.Background = Brushes.WhiteSmoke;

            DropShadowBitmapEffect myDropShadowEffect = new DropShadowBitmapEffect();
            myDropShadowEffect.Color = Colors.Black;
            myDropShadowEffect.Direction = 320;
            myDropShadowEffect.ShadowDepth = 5;
            myDropShadowEffect.Softness = 1;
            myDropShadowEffect.Opacity = 0.25;
            toggleButton.BitmapEffect = myDropShadowEffect;

            toggleButton.SetValue(FrameworkElement.NameProperty, text.ENG + "_ToggleButton");
            if (null != mainWindow.FindName(text.ENG + "_ToggleButton"))
            {
                mainWindow.UnregisterName(text.ENG + "_ToggleButton");
            }
            mainWindow.RegisterName(text.ENG + "_ToggleButton", toggleButton);
            toggleButton.Checked += TankToggleButtonChecked;
            toggleButton.Unchecked += TankToggleButtonUnchecked;

            Grid.SetRow(toggleButton, row);
            Grid.SetColumn(toggleButton, column);

            return toggleButton;
        }

        private void TankToggleButtonUnchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = (ToggleButton)sender;


            switch (toggleButton.Name)
            {
                case "Gasoline_ToggleButton":
                    TextBox GasolineTankTextBox = (TextBox)mainWindow.FindName("GasolineTank_Textbox");
                    toggleButton.Foreground = (Brush)Converter.ConvertFromString(LightTextColor);
                    GasolineTankTextBox.Visibility = Visibility.Hidden;

                    break;

                case "Diesel_ToggleButton":
                    TextBox DieselTankTextBox = (TextBox)mainWindow.FindName("DieselTank_Textbox");
                    toggleButton.Foreground = (Brush)Converter.ConvertFromString(LightTextColor);
                    DieselTankTextBox.Visibility = Visibility.Hidden;

                    break;

                case "LPG_ToggleButton":
                    TextBox LPGTankTextBox = (TextBox)mainWindow.FindName("LPGTank_Textbox");
                    toggleButton.Foreground = (Brush)Converter.ConvertFromString(LightTextColor);
                    LPGTankTextBox.Visibility = Visibility.Hidden;

                    break;
            }
        }

        private void TankToggleButtonChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = (ToggleButton)sender;

            TextBox GasolineTankTextBox = (TextBox)mainWindow.FindName("GasolineTank_Textbox");
            TextBox LPGTankTextBox = (TextBox)mainWindow.FindName("LPGTank_Textbox");
            TextBox DieselTankTextBox = (TextBox)mainWindow.FindName("DieselTank_Textbox");

            ToggleButton GasolineToggleButton = (ToggleButton)mainWindow.FindName("Gasoline_ToggleButton");
            ToggleButton DieselToggleButton = (ToggleButton)mainWindow.FindName("Diesel_ToggleButton");
            ToggleButton LPGToggleButton = (ToggleButton)mainWindow.FindName("LPG_ToggleButton");

            switch (toggleButton.Name)
            {
                case "Gasoline_ToggleButton":
                    toggleButton.Foreground = (Brush)Converter.ConvertFromString(DarkTextColor);

                    GasolineTankTextBox.Visibility = Visibility.Visible;

                    DieselTankTextBox.Visibility = Visibility.Hidden;
                    DieselToggleButton.IsChecked = false;

                    break;

                case "Diesel_ToggleButton":
                    toggleButton.Foreground = (Brush)Converter.ConvertFromString(DarkTextColor);

                    DieselTankTextBox.Visibility = Visibility.Visible;

                    GasolineTankTextBox.Visibility = Visibility.Hidden;
                    GasolineToggleButton.IsChecked = false;

                    LPGTankTextBox.Visibility = Visibility.Hidden;
                    LPGToggleButton.IsChecked = false;

                    break;

                case "LPG_ToggleButton":
                    toggleButton.Foreground = (Brush)Converter.ConvertFromString(DarkTextColor);

                    LPGTankTextBox.Visibility = Visibility.Visible;

                    DieselTankTextBox.Visibility = Visibility.Hidden;
                    DieselToggleButton.IsChecked = false;

                    break;
            }
        }
    }
}
