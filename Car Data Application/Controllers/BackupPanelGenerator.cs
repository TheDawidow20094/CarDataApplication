using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
<<<<<<< HEAD
=======
using Newtonsoft.Json;
>>>>>>> CA-6-API
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Car_Data_Application.Controllers
{
    class BackupPanelGenerator : CarDataAppController
    {
        private string LastOpenedPage;
        private Grid GrayedGrid;

        private Storyboard EntryAnimationStoryboard = new Storyboard();
        private Storyboard ExitAnimationStoryboard = new Storyboard();

        public void PanelGenerator(MainWindow mw, User user, BackupPanel translation)
        {
            InitialAssignValue(mw, user);

            GrayedGrid = new();
            mainWindow.RegisterName("GrayedGrid", GrayedGrid);
            Grid.SetColumnSpan(GrayedGrid, 2);

            Grid BlackOpacityGrid = new();
            mainWindow.RegisterName("BlackOpacityGrid", BlackOpacityGrid);
            BlackOpacityGrid.Background = Brushes.Black;
            BlackOpacityGrid.Opacity = 0.6;
            BlackOpacityGrid.MouseLeftButtonDown += PageClose;

            Grid BackupWindowGrid = new();
            mainWindow.RegisterName("LoginWindowGrid", BackupWindowGrid);
            SetGridProps(ref BackupWindowGrid);

            BackupWindowGrid.Width = 400;
            BackupWindowGrid.Height = 320;
            BackupWindowGrid.HorizontalAlignment = HorizontalAlignment.Center;
            BackupWindowGrid.VerticalAlignment = VerticalAlignment.Center;

            TranslateTransform translateTransform = new();
            if (null != mainWindow.FindName("Transform"))
            {
                mainWindow.UnregisterName("Transform");
            }
            mainWindow.RegisterName("Transform", translateTransform);
            BackupWindowGrid.RenderTransform = translateTransform;

            GenerateAnimation(ref BackupWindowGrid, "Entry");
            GenerateAnimation(ref BackupWindowGrid, "Exit");

            for (int i = 0; i < 2; i++)
            {
                BackupWindowGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            List<int> RowsHeights = new List<int>() { 80, 70, 80, 70 };
            foreach (int RowHeigh in RowsHeights)
            {
                BackupWindowGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(RowHeigh) });
            }

            BackupWindowGrid.Children.Add(GenerateTextBlock(translation.ApiBackupTitle, PUser.UserLanguage , 0, 0, horizontalAlignment: HorizontalAlignment.Center, isTitleFontSize: 28, isTitle: true));
<<<<<<< HEAD
            BackupWindowGrid.Children.Add(GenerateButton(translation.ImportButton, PUser.UserLanguage, 1, 0, LightTextColor));
            BackupWindowGrid.Children.Add(GenerateButton(translation.ExportButton, PUser.UserLanguage, 1, 1, LightTextColor));

            BackupWindowGrid.Children.Add(GenerateTextBlock(translation.GoogleBackupTitle, PUser.UserLanguage, 2, 0, horizontalAlignment: HorizontalAlignment.Center, isTitleFontSize: 28, isTitle: true));
            BackupWindowGrid.Children.Add(GenerateButton(translation.ImportButton, PUser.UserLanguage, 3, 0, LightTextColor));
            BackupWindowGrid.Children.Add(GenerateButton(translation.ExportButton, PUser.UserLanguage, 3, 1, LightTextColor));
=======
            BackupWindowGrid.Children.Add(GenerateButtonWithHandler(translation.ImportButton, PUser.UserLanguage, 1, 0, LightTextColor, "ApiImpot"));
            BackupWindowGrid.Children.Add(GenerateButtonWithHandler(translation.ExportButton, PUser.UserLanguage, 1, 1, LightTextColor, "ApiExport"));

            BackupWindowGrid.Children.Add(GenerateTextBlock(translation.GoogleBackupTitle, PUser.UserLanguage, 2, 0, horizontalAlignment: HorizontalAlignment.Center, isTitleFontSize: 28, isTitle: true));
            BackupWindowGrid.Children.Add(GenerateButtonWithHandler(translation.ImportButton, PUser.UserLanguage, 3, 0, LightTextColor, "GoogleImport"));
            BackupWindowGrid.Children.Add(GenerateButtonWithHandler(translation.ExportButton, PUser.UserLanguage, 3, 1, LightTextColor, "GoogleExport"));
>>>>>>> CA-6-API

            GrayedGrid.Children.Add(BlackOpacityGrid);
            GrayedGrid.Children.Add(BackupWindowGrid);

            mainWindow.MainGrid.Children.Add(GrayedGrid);

            mainWindow.BeginStoryboard(EntryAnimationStoryboard);
        }

        private void InitialAssignValue(MainWindow mw, User user)
        {
            LastOpenedPage = mw.WhereAreYou;
            mw.WhereAreYou = "BackupPage";
            mainWindow = mw;
            PUser = user;
            SetButtonColor(mainWindow.WhereAreYou, (Grid)mainWindow.FindName("SidePanel"));
        }

        private async void PageClose(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mainWindow.BeginStoryboard(ExitAnimationStoryboard);
            await Task.Delay(500);

            mainWindow.MainGrid.Children.Remove((Grid)mainWindow.FindName("GrayedGrid"));
            mainWindow.UnregisterName("GrayedGrid");
            mainWindow.UnregisterName("BlackOpacityGrid");
            mainWindow.UnregisterName("LoginWindowGrid");
            mainWindow.OpenPage(LastOpenedPage);
        }

        private void GenerateAnimation(ref Grid LoginWindowGrid, string animationType)
        {
            double MoveForm = 0, MoveTo = 0, OpacityFrom = 0, OpacityTo = 0;
            Storyboard storyboard = new Storyboard();

            switch (animationType)
            {
                case "Entry":
                    storyboard = EntryAnimationStoryboard;
                    MoveForm = 700;
                    MoveTo = 0;
                    OpacityFrom = 0.0;
                    OpacityTo = 0.6;

                    break;

                case "Exit":
                    storyboard = ExitAnimationStoryboard;
                    MoveForm = 0;
                    MoveTo = -700;
                    OpacityFrom = 0.6;
                    OpacityTo = 0.0;

                    break;
            }

            DoubleAnimation MoveAnimation = new DoubleAnimation();
            MoveAnimation.From = MoveForm;
            MoveAnimation.To = MoveTo;
            MoveAnimation.BeginTime = new TimeSpan(0, 0, 0);
            MoveAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(250));

            Storyboard.SetTargetName(MoveAnimation, "Transform");
            Storyboard.SetTargetProperty(MoveAnimation, new PropertyPath(TranslateTransform.XProperty));

            storyboard.Children.Clear();
            storyboard.Children.Add(MoveAnimation);
        }
<<<<<<< HEAD
=======

        private Button GenerateButtonWithHandler(Translation text, string language, int row, int column, string foregroundcolor, string buttonname)
        {
            Button button = GenerateButton(text, language, row, column, foregroundcolor, buttonName: buttonname);
            button.Click += ButtonClick;

            return button;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            switch (button.Name)
            {
                case "ApiImpot":

                    string ApiResponse = HttpGet("https://localhost:7074/api/getuser?dbpassword=" + PDbPassword + "&login=" + PUser.Login + "&password=" + PUser.Password);
                    if (HttpCheckRequest(ApiResponse) != "false")
                    {
                        PUser = JsonConvert.DeserializeObject<User>(ApiResponse);
                        PUser.SerializeData();
                        RefreshApp();
                        MessageBox.Show("Import Successful");
                    }
                    break;

                case "ApiExport":

                    if (HttpCheckRequest(HttpPost("https://localhost:7074/api/editjson?dbpassword=" + PDbPassword + "&id=" + PUser.Id, PUser.SerializeData(PUser))) != "false")
                    {
                        MessageBox.Show("Export Successful");
                    }
                    break;

                case "GoogleImport":
                    break;

                case "GoogleExport":
                    break;
            }
        }
>>>>>>> CA-6-API
    }
}
