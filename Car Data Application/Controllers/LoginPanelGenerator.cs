using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Net.Http;
using Newtonsoft.Json;

namespace Car_Data_Application.Controllers
{
    class LoginPanelGenerator : CarDataAppController
    {
        private string LastOpenedPage;
        private LoginPanel translation;
        private Grid GrayedGrid;

        private Storyboard EntryAnimationStoryboard = new Storyboard();
        private Storyboard ExitAnimationStoryboard = new Storyboard();
        private Storyboard GotoOtherPageAnimationStoryboard = new Storyboard();
        private Storyboard myWidthAnimatedButtonStoryboard = new Storyboard();

        private bool PSendToApi = true;

        public void PageGenerator(MainWindow mw, User user, Config paramConfig)
        {
            InitialAssignValue(mw, user, paramConfig);

            GrayedGrid = new();
            mainWindow.RegisterName("GrayedGrid", GrayedGrid);
            Grid.SetColumnSpan(GrayedGrid, 2);

            Grid BlackOpacityGrid = new();
            mainWindow.RegisterName("BlackOpacityGrid", BlackOpacityGrid);
            BlackOpacityGrid.Background = Brushes.Black;
            BlackOpacityGrid.Opacity = 0.6;
            BlackOpacityGrid.MouseLeftButtonDown += PageClose;

            Grid LoginWindowGrid = new();
            mainWindow.RegisterName("LoginWindowGrid", LoginWindowGrid);
            SetGridProps(ref LoginWindowGrid);

            LoginWindowGrid.Width = 350;
            LoginWindowGrid.Height = 350;
            LoginWindowGrid.HorizontalAlignment = HorizontalAlignment.Center;
            LoginWindowGrid.VerticalAlignment = VerticalAlignment.Center;

            TranslateTransform translateTransform = new();
            if (null != mainWindow.FindName("Transform"))
            {
                mainWindow.UnregisterName("Transform");
            }
            mainWindow.RegisterName("Transform", translateTransform);
            LoginWindowGrid.RenderTransform = translateTransform;

            GenerateAnimation(ref LoginWindowGrid, "Entry");
            GenerateAnimation(ref LoginWindowGrid, "Exit");
            GenerateAnimation(ref LoginWindowGrid, "OtherPage");

            for (int i = 0; i < 2; i++)
            {
                LoginWindowGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            List<int> RowsHeights = new List<int>() { 70, 60, 60, 70, 70 };
            foreach (int RowHeigh in RowsHeights)
            {
                LoginWindowGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(RowHeigh) });
            }

            LoginWindowGrid.Children.Add(GenerateTextBlock(translation.LoginTitle, PUser.UserLanguage, 0, 0, horizontalAlignment: HorizontalAlignment.Center, isTitleFontSize: 28, isTitle: true));
            LoginWindowGrid.Children.Add(GenerateTextBlock(translation.UserName, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));
            LoginWindowGrid.Children.Add(GenerateTextBlock(translation.Password, PUser.UserLanguage, 2, 0, LightTextColor, HorizontalAlignment.Right));

            LoginWindowGrid.Children.Add(GenerateTextBox("UserName", 1, 1));
            LoginWindowGrid.Children.Add(GenerateTextBox("Password", 2, 1));

            Button LoginButton = GenerateButton(translation.LogInButton, PUser.UserLanguage, 3, 0);
            Grid.SetColumnSpan(LoginButton, 2);
            LoginButton.Click += LoginButtonClick;
            LoginWindowGrid.Children.Add(LoginButton);

            Button RegisterButton = GenerateButton(translation.RegisterButton, PUser.UserLanguage, 4, 0);
            Grid.SetColumnSpan(RegisterButton, 2);
            RegisterButton.Click += RegisterButtonClick; ;
            LoginWindowGrid.Children.Add(RegisterButton);


            GrayedGrid.Children.Add(BlackOpacityGrid);
            GrayedGrid.Children.Add(LoginWindowGrid);

            mainWindow.MainGrid.Children.Add(GrayedGrid);

            mainWindow.BeginStoryboard(EntryAnimationStoryboard);
        }

        private void InitialAssignValue(MainWindow mw, User user, Config paramConfig)
        {
            LastOpenedPage = mw.WhereAreYou;
            mw.WhereAreYou = "LoginPage";
            mainWindow = mw;
            PUser = user;
            config = paramConfig;
            translation = config.MainPanel.LoginPanel;
            SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.FindName("SidePanel")));
        }

        private async void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.BeginStoryboard(GotoOtherPageAnimationStoryboard);
            await Task.Delay(500);

            GrayedGrid.Children.Remove((Grid)mainWindow.FindName("LoginWindowGrid"));
            new RegisterPanelGenerator().PageGenerator(mainWindow, PUser, config.MainPanel.RegisterPanel, LastOpenedPage, GrayedGrid);
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            TextBox UserName_TextBox = (TextBox)mainWindow.FindName("UserName_TextBox");
            TextBox Password_TextBox = (TextBox)mainWindow.FindName("Password_TextBox");


            UserName_TextBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
            Password_TextBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);

            if (UserName_TextBox.Text == "")
            {
                ((TextBox)mainWindow.FindName("UserName_TextBox")).Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                PSendToApi = false;
            }
            if (Password_TextBox.Text == "")
            {
                ((TextBox)mainWindow.FindName("Password_TextBox")).Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                PSendToApi = false;
            }
            if(PSendToApi)
            {
                string ApiResponse = HttpGet("https://localhost:7074/api/getuser?dbpassword=" + PDbPassword + "&login=" + UserName_TextBox.Text + "&password=" + Password_TextBox.Text);

                if (ApiResponse == "No user found" || ApiResponse == "")
                {
                    MessageBox.Show("Nie udało się zalogować!");
                    
                }
                else
                {
                    PUser = JsonConvert.DeserializeObject<User>(ApiResponse);
                    PUser.SerializeData();
                    RefreshApp();
                }

            }
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

                case "OtherPage":
                    storyboard = GotoOtherPageAnimationStoryboard;
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

            if (animationType != "OtherPage")
            {
                DoubleAnimation OpacityAnimation = new DoubleAnimation();
                OpacityAnimation.From = OpacityFrom;
                OpacityAnimation.To = OpacityTo;
                OpacityAnimation.BeginTime = new TimeSpan(0, 0, 0);
                OpacityAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));


                Storyboard.SetTargetName(OpacityAnimation, "BlackOpacityGrid");
                Storyboard.SetTargetProperty(OpacityAnimation, new PropertyPath(Grid.OpacityProperty));

                storyboard.Children.Add(OpacityAnimation);
            }
        }

        //private TextBox GenerateTextBoxWithHandler(string textboxname, int row, int column)
        //{
        //    TextBox textBox = GenerateTextBox(textboxname, row, column);
        //    textBox.LostFocus += TextBoxLostFocus;
        //    return textBox;
        //}

        //private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        //{
        //    TextBox textBox = (TextBox)sender;
        //    textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);

        //    if (HttpGet("https://localhost:7074/api/checksernameexist?dbpassword=" + PDbPassword + "&login=" + textBox.Text) != "Username is available")
        //    {
        //        PSendToApi = false;
        //        textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
        //    }
        //}
    }
}
