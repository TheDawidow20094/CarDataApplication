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

        public Storyboard myWidthAnimatedButtonStoryboard = new Storyboard();

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

            TranslateTransform translateTransform = new TranslateTransform();
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
            LoginWindowGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70) });
            LoginWindowGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60) });
            LoginWindowGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60) });
            LoginWindowGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70) });
            LoginWindowGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70) });

            switch (PUser.UserLanguage)
            {
                case "PL":
                    LoginWindowGrid.Children.Add(GenerateTextBlock(translation.LoginTitle.PL, 0, 0, horizontalAlignment: HorizontalAlignment.Center, isTitleFontSize: 28, isTitle: true));
                    LoginWindowGrid.Children.Add(GenerateTextBlock(translation.UserName.PL, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    LoginWindowGrid.Children.Add(GenerateTextBlock(translation.Password.PL, 2, 0, LightTextColor, HorizontalAlignment.Right));

                    break;

                case "ENG":
                    LoginWindowGrid.Children.Add(GenerateTextBlock(translation.LoginTitle.ENG, 0, 0, horizontalAlignment: HorizontalAlignment.Center, isTitleFontSize: 28, isTitle: true));
                    LoginWindowGrid.Children.Add(GenerateTextBlock(translation.UserName.ENG, 1, 0, LightTextColor, HorizontalAlignment.Right));
                    LoginWindowGrid.Children.Add(GenerateTextBlock(translation.Password.ENG, 2, 0, LightTextColor, HorizontalAlignment.Right));

                    break;
            }

            LoginWindowGrid.Children.Add(GenerateTextBox("UserName", 1, 1));
            LoginWindowGrid.Children.Add(GenerateTextBox("Password", 2, 1));

            Button LoginButton = GenerateButton(translation.LogInButton, PUser.UserLanguage, 3 ,0);
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
            MessageBox.Show("Logowanie");
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
    }
}
