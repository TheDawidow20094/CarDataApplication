using Car_Data_Application.Models;
using Car_Data_Application.Models.XML_Models;
using Car_Data_Application.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Car_Data_Application.Controllers
{
    class RegisterPanelGenerator :CarDataAppController
    {
        private string LastOpenedPage;
        private Grid GrayedGrid;

        private Storyboard EntryAnimationStoryboard = new();
        private Storyboard ExitAnimationStoryboard = new();
        private Storyboard myWidthAnimatedButtonStoryboard = new Storyboard();

        private bool PSendToApi;

        public void PageGenerator(MainWindow mw, User user, RegisterPanel translation, string lastOpenedPage, Grid grayedGrid)
        {
            InitialAssignValue(mw, user, lastOpenedPage, grayedGrid);

            Grid RegisterWindowGrid = new();
            SetGridProps(ref RegisterWindowGrid);

            RegisterWindowGrid.Width = 350;
            RegisterWindowGrid.Height = 415;
            RegisterWindowGrid.HorizontalAlignment = HorizontalAlignment.Center;
            RegisterWindowGrid.VerticalAlignment = VerticalAlignment.Center;

            TranslateTransform translateTransform = new TranslateTransform();
            if (null != mainWindow.FindName("Transform"))
            {
                mainWindow.UnregisterName("Transform");
            }
            mainWindow.RegisterName("Transform", translateTransform);
            RegisterWindowGrid.RenderTransform = translateTransform;

            GenerateAnimation(ref RegisterWindowGrid, "Entry");
            GenerateAnimation(ref RegisterWindowGrid, "Exit");

            for (int i = 0; i < 2; i++)
            {
                RegisterWindowGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            List<int> Rows = new List<int>{ 70 , 60, 60, 60, 70, 70 };
            foreach (int row in Rows)
            {
                RegisterWindowGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(row) });
            }

            RegisterWindowGrid.Children.Add(GenerateTextBlock(translation.RegisterTitle, PUser.UserLanguage, 0, 0, horizontalAlignment: HorizontalAlignment.Center, isTitleFontSize: 28, isTitle: true));
            RegisterWindowGrid.Children.Add(GenerateTextBlock(translation.UserName, PUser.UserLanguage, 1, 0, LightTextColor, HorizontalAlignment.Right));
            RegisterWindowGrid.Children.Add(GenerateTextBlock(translation.Password, PUser.UserLanguage, 2, 0, LightTextColor, HorizontalAlignment.Right));
            RegisterWindowGrid.Children.Add(GenerateTextBlock(translation.RePassword, PUser.UserLanguage, 3, 0, LightTextColor, HorizontalAlignment.Right));
            RegisterWindowGrid.Children.Add(GenerateTextBlock(translation.Email, PUser.UserLanguage, 4, 0, LightTextColor, HorizontalAlignment.Right));

            RegisterWindowGrid.Children.Add(GenerateTextBoxWithHandler("UserName", 1, 1));
            RegisterWindowGrid.Children.Add(GeneratePasswordBox("Password", 2, 1));
            RegisterWindowGrid.Children.Add(GeneratePasswordBox("RePassword", 3, 1));
            RegisterWindowGrid.Children.Add(GenerateTextBox("Email", 4, 1));

            Button RegisterButton = GenerateButton(translation.RegisterButton, PUser.UserLanguage, 5, 0);
            Grid.SetColumnSpan(RegisterButton, 2);
            RegisterButton.Click += RegisterButtonClick; ;
            RegisterWindowGrid.Children.Add(RegisterButton);


            GrayedGrid.Children.Add(RegisterWindowGrid);

            mainWindow.BeginStoryboard(EntryAnimationStoryboard);
        }

        private void InitialAssignValue(MainWindow mw, User user, string lastOpenedPage, Grid grayedGrid)
        {
            LastOpenedPage = mw.WhereAreYou;
            mw.WhereAreYou = "LoginPage";
            mainWindow = mw;
            PUser = user;
            LastOpenedPage = lastOpenedPage;
            GrayedGrid = grayedGrid;
            SetButtonColor(mainWindow.WhereAreYou, (Grid)mainWindow.FindName("SidePanel"));
        }

        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            PSendToApi = true;

            TextBox UserName_TextBox = (TextBox)mainWindow.FindName("UserName_TextBox");
            PasswordBox Password_PasswordBox = (PasswordBox)mainWindow.FindName("Password_PasswordBox");
            PasswordBox RePassword_PasswordBox = (PasswordBox)mainWindow.FindName("RePassword_PasswordBox");
            TextBox Email_TextBox = (TextBox)mainWindow.FindName("Email_TextBox");


            Password_PasswordBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
            RePassword_PasswordBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);
            if (Password_PasswordBox.Password != RePassword_PasswordBox.Password)
            {
                Password_PasswordBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                RePassword_PasswordBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
                PSendToApi = false;
            }
            if (PSendToApi)
            {
                PUser.Login = UserName_TextBox.Text;
                PUser.Password = Password_PasswordBox.Password;
                PUser.Email = Email_TextBox.Text;

                string json = JsonSerializer.Serialize(PUser);
                if (HttpCheckRequest(HttpPost("https://localhost:7074/api/adduser?dbpassword=" + PDbPassword, json)) != "false")
                {
                    PUser.SerializeData();

                    string ApiResponse = HttpGet("https://localhost:7074/api/getuser?dbpassword=" + PDbPassword + "&login=" + PUser.Login + "&password=" + PUser.Password);
                    if (HttpCheckRequest(ApiResponse) != "false")
                    {
                        PUser = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(ApiResponse);
                        PUser.SerializeData();
                        RefreshApp();
                    }
                }
                
            }
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

            if (animationType == "Exit")
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

        private TextBox GenerateTextBoxWithHandler(string textboxname, int row, int column)
        {
            TextBox textBox = GenerateTextBox(textboxname, row, column);
            textBox.LostFocus += TextBoxLostFocus;
            return textBox;
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundColor);

            if (HttpGet("https://localhost:7074/api/checksernameexist?dbpassword=" + PDbPassword + "&login=" + textBox.Text) != "Username is available")
            {
                PSendToApi = false;
                textBox.Background = (Brush)Converter.ConvertFromString(TextBoxBackgroundRedColor);
            }
        }
    }
}
