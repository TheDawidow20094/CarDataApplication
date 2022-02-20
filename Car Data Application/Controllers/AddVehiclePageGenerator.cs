using Car_Data_Application.Models;
using Car_Data_Application.Models.Vehicle_Classes;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Car_Data_Application.Controllers
{
    class AddVehiclePageGenerator : CarDataAppController
    {
        private Vehicle newVehicle;
        private Grid FuelInfoGrid;
        private Grid AddImageGrid;

        public void PageGenerator(MainWindow mw, User user, Config paramConfig)
        {
            InitialAssignValue(mw, user, paramConfig);
            newVehicle = new Vehicle();
            Grid MainGrid = new Grid();

            List<int> RowsHeights = new List<int>() { 50, 100, 160, 150, 170, 80 };
            foreach (int RowHeight in RowsHeights)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(RowHeight) });
            }

            MainGrid.Children.Add(AddingTitle(config.MainPanel.AddVehiclePage));
            MainGrid.Children.Add(AddingVehiclePrimaryDataContent(config.MainPanel.AddVehiclePage));
            MainGrid.Children.Add(AddingPrimaryInfoContent(config.MainPanel.AddVehiclePage));
            MainGrid.Children.Add(AddingFuelTankInfoContent(config.MainPanel.AddVehiclePage));
            MainGrid.Children.Add(AddingCyclicalCostContent(config.MainPanel.AddVehiclePage));
            MainGrid.Children.Add(AddRefuelingButton(config.MainPanel.AddVehiclePage));


            newVehicle.Id = AutoincrementVehicleID();
            mainWindow.ScrollViewerContent.Content = MainGrid;

        }

        private void InitialAssignValue(MainWindow mw, User user, Config paramConfig)
        {
            config = paramConfig;
            mainWindow = mw;
            PUser = user;
            mainWindow.AddButon.Visibility = Visibility.Hidden;
            SetButtonColor("VehiclesPage", ((Grid)mainWindow.FindName("SidePanel")));
        }

        private Grid AddingTitle(AddVehiclePage translation)
        {
            Grid TitleGrid = new Grid();
            TitleGrid.Children.Add(GenerateTextBlock(translation.Title, PUser.UserLanguage, 0, 0, horizontalAlignment: HorizontalAlignment.Center, verticalAlignment: VerticalAlignment.Center, isTitle: true));

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

            AddImageGrid = new();
            AddImageGrid.MouseLeftButtonDown += AddImageClick;

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
            AddPictureText.Margin = new Thickness(0,0,0,15);
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

            VehicleNameGrid.Children.Add(GenerateTextBlock(translation.Brand, PUser.UserLanguage, 0, 0, LightTextColor, HorizontalAlignment.Right));
            VehicleNameGrid.Children.Add(GenerateTextBlock(translation.Model, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));

            VehicleNameGrid.Children.Add(GenerateTextBox("Brand" , 0, 1, smallersize: true, horizontalAlignment: HorizontalAlignment.Left));
            VehicleNameGrid.Children.Add(GenerateTextBox("Model" , 1, 1, smallersize: true, horizontalAlignment: HorizontalAlignment.Left));

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

            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.YearOfManufacture, PUser.UserLanguage, 0, 0, LightTextColor, HorizontalAlignment.Right, VerticalAlignment.Center));
            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.VIN, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right, VerticalAlignment.Center));
            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.Plates, PUser.UserLanguage, 2, 0, LightTextColor, HorizontalAlignment.Right, VerticalAlignment.Center));
            PrimarmaryInfoGrid.Children.Add(GenerateTextBlock(translation.VehicleMillage, PUser.UserLanguage, 3, 0, LightTextColor, HorizontalAlignment.Right, VerticalAlignment.Center));

            PrimarmaryInfoGrid.Children.Add(GenerateTextBoxWithHandler("YearOfManufacture", 0, 1, horizontalAlignment: HorizontalAlignment.Left));
            PrimarmaryInfoGrid.Children.Add(GenerateTextBox("VIN", 1, 1, horizontalAlignment: HorizontalAlignment.Left));
            PrimarmaryInfoGrid.Children.Add(GenerateTextBox("Plates", 2, 1, horizontalAlignment: HorizontalAlignment.Left));
            PrimarmaryInfoGrid.Children.Add(GenerateTextBoxWithHandler("VehicleMillage", 3, 1, horizontalAlignment: HorizontalAlignment.Left));

            return PrimarmaryInfoGrid;
        }

        private Grid AddingFuelTankInfoContent(AddVehiclePage translation)
        {
            FuelInfoGrid = new Grid();
            SetGridProps(ref FuelInfoGrid, 3);

            for (int i = 0; i < 3; i++) // 3 number of columns
            {
                FuelInfoGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            FuelInfoGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(80)});
            FuelInfoGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });


            FuelInfoGrid.Children.Add(GenerateToggleButtonWithHandlers(translation.GasolineTank, PUser.UserLanguage, 0, 0));
            FuelInfoGrid.Children.Add(GenerateToggleButtonWithHandlers(translation.LPGTank, PUser.UserLanguage, 0, 1));
            FuelInfoGrid.Children.Add(GenerateToggleButtonWithHandlers(translation.DieselTank, PUser.UserLanguage, 0, 2));


            FuelInfoGrid.Children.Add(GenerateTextBoxWithHandler("GasolineTank", 1, 0, horizontalAlignment: HorizontalAlignment.Center, visibility: Visibility.Hidden));
            FuelInfoGrid.Children.Add(GenerateTextBoxWithHandler("LPGTank", 1, 1, horizontalAlignment: HorizontalAlignment.Center, visibility: Visibility.Hidden));
            FuelInfoGrid.Children.Add(GenerateTextBoxWithHandler("DieselTank", 1, 2, horizontalAlignment: HorizontalAlignment.Center, visibility: Visibility.Hidden));


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

            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceTitle, PUser.UserLanguage, 0, 0, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceStartDate, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsuranceEndDate, PUser.UserLanguage, 2, 0, LightTextColor, HorizontalAlignment.Right));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InsurancePrice, PUser.UserLanguage, 3, 0, LightTextColor, HorizontalAlignment.Right));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionTitle, PUser.UserLanguage, 0, 2, DarkTextColor, HorizontalAlignment.Center, VerticalAlignment.Center, true));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionStartDate, PUser.UserLanguage, 1, 2, LightTextColor, HorizontalAlignment.Right));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionEndDate, PUser.UserLanguage, 2, 2, LightTextColor, HorizontalAlignment.Right));
            CyclicalCostGrid.Children.Add(GenerateTextBlock(translation.InspectionPrice, PUser.UserLanguage, 3, 2, LightTextColor, HorizontalAlignment.Right));

            CyclicalCostGrid.Children.Add(GenerateTextBox("InsuranceStartDate", 1, 1, horizontalAlignment: HorizontalAlignment.Left));
            CyclicalCostGrid.Children.Add(GenerateTextBox("InsuranceEndDate", 2, 1, horizontalAlignment: HorizontalAlignment.Left));
            CyclicalCostGrid.Children.Add(GenerateTextBox("InsurancePrice", 3, 1, horizontalAlignment: HorizontalAlignment.Left));

            CyclicalCostGrid.Children.Add(GenerateTextBox("InspectionStartDate", 1, 3, horizontalAlignment: HorizontalAlignment.Left));
            CyclicalCostGrid.Children.Add(GenerateTextBox("InspectionEndDate", 2, 3, horizontalAlignment: HorizontalAlignment.Left));
            CyclicalCostGrid.Children.Add(GenerateTextBox("InspectionPrice", 3, 3, horizontalAlignment: HorizontalAlignment.Left));

            return CyclicalCostGrid;
        }

        private Button AddRefuelingButton(AddVehiclePage translation)
        {
            Button ApplySettingsButton = GenerateButton(translation.AddButton, PUser.UserLanguage, 5, 0, DarkTextColor);
            ApplySettingsButton.Background = (Brush)Converter.ConvertFromString("#FF93D68A");
            ApplySettingsButton.Height = 60;
            ApplySettingsButton.Width = 200;
            ApplySettingsButton.Click += HandleAddVehicleButtonClick;

            return ApplySettingsButton;
        }

        private CyclicalCosts GenerateCyclicalCost(ref bool CanConvertToJson, TextBox StartDate, TextBox EndDate, TextBox Price)
        {
            int ParseResult;

            CyclicalCosts newCyclicalCost = null;
            int InsurancePrice = 0;
            if (int.TryParse(Price.Text, out ParseResult))
            {
                InsurancePrice = ParseResult;
                Price.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
                if (StartDate.Text == "")
                {
                    CanConvertToJson = false;
                    StartDate.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                }
                else { StartDate.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor); }
                if (EndDate.Text == "")
                {
                    CanConvertToJson = false;
                    EndDate.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                }
                else { EndDate.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor); }
                if ((StartDate.Text != "") && (EndDate.Text != ""))
                {
                    newCyclicalCost = new CyclicalCosts(StartDate.Text, EndDate.Text, InsurancePrice);
                }
            }
            else
            {
                if (Price.Text != "")
                {
                    CanConvertToJson = false;
                    Price.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                    if (StartDate.Text == "") { StartDate.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor); }
                    if (EndDate.Text == "") { EndDate.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor); }
                }
                if ((Price.Text == "") && ((StartDate.Text != "") || (EndDate.Text != "")))
                {
                    CanConvertToJson = false;
                    Price.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                    if (StartDate.Text == "")
                    {
                        StartDate.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                    }
                    else { StartDate.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor); }
                    if (EndDate.Text == "")
                    {
                        EndDate.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                    }
                    else { EndDate.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor); }
                }
            }
            return newCyclicalCost;
        }

        private ToggleButton GenerateToggleButtonWithHandlers(Translation text, string language, int row, int column)
        {
            ToggleButton toggleButton = GenerateToggleButton(text, language, row, column);
            
            toggleButton.Unchecked += TankToggleButtonUnchecked;
            toggleButton.Checked += TankToggleButtonChecked;

            return toggleButton;
        }

        private TextBox GenerateTextBoxWithHandler(string textboxname, int row, int column, HorizontalAlignment horizontalAlignment, Visibility visibility = Visibility.Visible)
        {
            TextBox textBox = GenerateTextBox(textboxname, row, column, horizontalAlignment: horizontalAlignment, visibility: visibility);
            textBox.TextChanged += TextBoxTextChange;

            return textBox;
        }

        private void TextBoxTextChange(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int ParseResult;

            if (int.TryParse(textBox.Text, out ParseResult))
            {
                textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);

                if ((textBox.Name == "GasolineTank_TextBox") || (textBox.Name == "LPGTank_TextBox") || (textBox.Name == "DieselTank_TextBox"))
                {
                    FuelInfoGrid.Background = Brushes.WhiteSmoke;

                    switch (textBox.Name)
                    {
                        case "GasolineTank_TextBox":
                            newVehicle.Tanks.Gasoline = ParseResult;
                            break;

                        case "LPGTank_TextBox":
                            newVehicle.Tanks.LPG = ParseResult;
                            break;

                        case "DieselTank_TextBox":
                            newVehicle.Tanks.Diesel = ParseResult;
                            break;
                    }
                }
                else
                {
                    switch (textBox.Name)
                    {
                        case "YearOfManufacture_TextBox":
                            newVehicle.YearOfManufacture = ParseResult;
                            break;

                        case "VehicleMillage_TextBox":
                            newVehicle.CarMillage = ParseResult;
                            break;
                    }
                }
            }
            else
            {
                textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
            }
        }

        private void TankToggleButtonUnchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = (ToggleButton)sender;


            switch (toggleButton.Name)
            {
                case "Gasoline_ToggleButton":
                    TextBox GasolineTankTextBox = (TextBox)mainWindow.FindName("GasolineTank_TextBox");
                    toggleButton.Foreground = (Brush)Converter.ConvertFromString(LightTextColor);
                    GasolineTankTextBox.Visibility = Visibility.Hidden;

                    newVehicle.Tanks.Gasoline = 0;
                    break;

                case "Diesel_ToggleButton":
                    TextBox DieselTankTextBox = (TextBox)mainWindow.FindName("DieselTank_TextBox");
                    toggleButton.Foreground = (Brush)Converter.ConvertFromString(LightTextColor);
                    DieselTankTextBox.Visibility = Visibility.Hidden;

                    newVehicle.Tanks.Diesel = 0;
                    break;

                case "LPG_ToggleButton":
                    TextBox LPGTankTextBox = (TextBox)mainWindow.FindName("LPGTank_TextBox");
                    toggleButton.Foreground = (Brush)Converter.ConvertFromString(LightTextColor);
                    LPGTankTextBox.Visibility = Visibility.Hidden;

                    newVehicle.Tanks.LPG = 0;
                    break;
            }
        }

        private void TankToggleButtonChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = (ToggleButton)sender;

            TextBox GasolineTankTextBox = (TextBox)mainWindow.FindName("GasolineTank_TextBox");
            TextBox LPGTankTextBox = (TextBox)mainWindow.FindName("LPGTank_TextBox");
            TextBox DieselTankTextBox = (TextBox)mainWindow.FindName("DieselTank_TextBox");

            ToggleButton GasolineToggleButton = (ToggleButton)mainWindow.FindName("Gasoline_ToggleButton");
            ToggleButton DieselToggleButton = (ToggleButton)mainWindow.FindName("Diesel_ToggleButton");
            ToggleButton LPGToggleButton = (ToggleButton)mainWindow.FindName("LPG_ToggleButton");

            int ParseResult;
            switch (toggleButton.Name)
            {
                case "Gasoline_ToggleButton":
                    toggleButton.Foreground = (Brush)Converter.ConvertFromString(DarkTextColor);

                    GasolineTankTextBox.Visibility = Visibility.Visible;

                    DieselTankTextBox.Visibility = Visibility.Hidden;
                    DieselToggleButton.IsChecked = false;

                    if (int.TryParse(GasolineTankTextBox.Text, out ParseResult))
                    {
                        newVehicle.Tanks.Gasoline = ParseResult;
                        FuelInfoGrid.Background = Brushes.WhiteSmoke;
                    }
                    newVehicle.Tanks.Diesel = 0;
                    break;

                case "Diesel_ToggleButton":
                    toggleButton.Foreground = (Brush)Converter.ConvertFromString(DarkTextColor);

                    DieselTankTextBox.Visibility = Visibility.Visible;

                    GasolineTankTextBox.Visibility = Visibility.Hidden;
                    GasolineToggleButton.IsChecked = false;

                    LPGTankTextBox.Visibility = Visibility.Hidden;
                    LPGToggleButton.IsChecked = false;

                    if (int.TryParse(DieselTankTextBox.Text, out ParseResult))
                    {
                        newVehicle.Tanks.Diesel = ParseResult;
                        FuelInfoGrid.Background = Brushes.WhiteSmoke;
                    }
                    newVehicle.Tanks.Gasoline = 0;
                    newVehicle.Tanks.LPG = 0;
                    break;

                case "LPG_ToggleButton":
                    toggleButton.Foreground = (Brush)Converter.ConvertFromString(DarkTextColor);

                    LPGTankTextBox.Visibility = Visibility.Visible;

                    DieselTankTextBox.Visibility = Visibility.Hidden;
                    DieselToggleButton.IsChecked = false;

                    if (int.TryParse(LPGTankTextBox.Text, out ParseResult))
                    {
                        newVehicle.Tanks.LPG = ParseResult;
                        FuelInfoGrid.Background = Brushes.WhiteSmoke;
                    }

                    newVehicle.Tanks.Diesel = 0;
                    break;
            }
        }

        private void AddImageClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            //Vehicle vehicle = PUser.Vehicles[PUser.ActiveCarIndex];
            string newPictureName = "Vehicle_ID_" + newVehicle.Id + "_VehiclePicture.PNG";
            string PicturePath = @"..\..\..\Images\UserPictures\" + newPictureName;

            BitmapImage photo;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Files (*.JPEG;*.JPG;*.JPE;*.JFIF)|*.JPEG;*.JPG;*.JPE;*.JFIF| PNG Files (*.PNG)|*.PNG";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                photo = new BitmapImage(fileUri);

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(photo));


                using (FileStream stream = new FileStream(PicturePath, FileMode.Create))
                {
                    encoder.Save(stream);
                    stream.Close();
                    newVehicle.PictureFileName = newPictureName;
                }

                Rectangle rectangle = (Rectangle)AddImageGrid.Children[0];
                rectangle.Stroke = (Brush)Converter.ConvertFromString(DarkTextColor);
                ((TextBlock)AddImageGrid.Children[1]).Foreground = (Brush)Converter.ConvertFromString(DarkTextColor);

                ImageBrush imageBrush = new ImageBrush(photo);
                imageBrush.Stretch = Stretch.UniformToFill;
                imageBrush.AlignmentY = AlignmentY.Center;
                rectangle.Fill = imageBrush;
            }
        }

        private int AutoincrementVehicleID()
        {
            int id = 0;
            if(PUser.Vehicles.Count != 0)
            {
                id = PUser.Vehicles[PUser.Vehicles.Count - 1].Id + 1;
            }

            return id;
        }

        private void HandleAddVehicleButtonClick(object sender, RoutedEventArgs e)
        {
            bool CanConvertToJson = true;


            TextBox Brand_TextBox = (TextBox)mainWindow.FindName("Brand_TextBox");
            TextBox Model_TextBox = (TextBox)mainWindow.FindName("Model_TextBox");
            TextBox VIN_TextBox = (TextBox)mainWindow.FindName("VIN_TextBox");
            TextBox Plates_TextBox = (TextBox)mainWindow.FindName("Plates_TextBox");

            newVehicle.Brand = Brand_TextBox.Text;
            newVehicle.Model = Model_TextBox.Text;
            newVehicle.Vin = VIN_TextBox.Text;
            newVehicle.Plates = Plates_TextBox.Text;

            if (Brand_TextBox.Text == "")
            {
                CanConvertToJson = false;
                Brand_TextBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
            }
            if (Model_TextBox.Text == "")
            {
                CanConvertToJson = false;
                Model_TextBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
            }
            if (newVehicle.YearOfManufacture == 0)
            {
                CanConvertToJson = false;
                ((TextBox)mainWindow.FindName("YearOfManufacture_TextBox")).Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
            }
            if (newVehicle.CarMillage == 0)
            {
                CanConvertToJson = false;
                ((TextBox)mainWindow.FindName("VehicleMillage_TextBox")).Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
            }
            if ((newVehicle.Tanks.Gasoline == 0) && (newVehicle.Tanks.LPG == 0) && (newVehicle.Tanks.Diesel == 0))
            {
                CanConvertToJson = false;
                FuelInfoGrid.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
            }


            TextBox InsuranceStartDate = (TextBox)mainWindow.FindName("InsuranceStartDate_TextBox");
            TextBox InsuranceEndDate = (TextBox)mainWindow.FindName("InsuranceEndDate_TextBox");
            TextBox InsurancePrice = (TextBox)mainWindow.FindName("InsurancePrice_TextBox");
            newVehicle.Insurance = GenerateCyclicalCost(ref CanConvertToJson, InsuranceStartDate, InsuranceEndDate, InsurancePrice);

            TextBox InspectionStartDate = (TextBox)mainWindow.FindName("InspectionStartDate_TextBox");
            TextBox InspectionEndDate = (TextBox)mainWindow.FindName("InspectionEndDate_TextBox");
            TextBox InspectionPrice = (TextBox)mainWindow.FindName("InspectionPrice_TextBox");
            newVehicle.Inspection = GenerateCyclicalCost(ref CanConvertToJson, InspectionStartDate, InspectionEndDate, InspectionPrice);

            if (CanConvertToJson)
            {
                PUser.Vehicles.Add(newVehicle);
                PUser.SerializeData();
                mainWindow.OpenPage("VehiclesPage");
            }
        }

    }
}
