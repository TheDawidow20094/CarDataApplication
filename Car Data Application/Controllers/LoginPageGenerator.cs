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
    class LoginPageGenerator : CarDataAppController
    {
        private string LastOpenedPage;

        private Storyboard EntryAnimationStoryboard = new Storyboard();
        private Storyboard ExitAnimationStoryboard = new Storyboard();

        public Storyboard myWidthAnimatedButtonStoryboard = new Storyboard();

        public void PageGenerator(MainWindow mw, User user, LoginPanel translation)
        {
            InitialAssignValue(mw, user);

            Grid GrayedGrid = new();
            GrayedGrid.MouseLeftButtonDown += LoginWindowClose;
            mainWindow.RegisterName("GrayedGrid", GrayedGrid);
            Grid.SetColumnSpan(GrayedGrid, 2);

            Grid BlackOpacityGrid = new();
            BlackOpacityGrid.Background = Brushes.Black;
            BlackOpacityGrid.Opacity = 0.4;

            Grid LoginWindowGrid = new();
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

            GenerateAnimation(ref LoginWindowGrid,ref EntryAnimationStoryboard, 700, 0);
            GenerateAnimation(ref LoginWindowGrid, ref ExitAnimationStoryboard, 0, -700);

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

        private void InitialAssignValue(MainWindow mw, User user)
        {
            LastOpenedPage = mw.WhereAreYou;
            mw.WhereAreYou = "LoginPage";
            mainWindow = mw;
            PUser = user;
            SetButtonColor(mainWindow.WhereAreYou, ((Grid)mainWindow.MainGrid.Children[3]));
        }

        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logowanie");
        }

        private async void LoginWindowClose(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mainWindow.BeginStoryboard(ExitAnimationStoryboard);
            
            await Task.Delay(500);

            mainWindow.MainGrid.Children.Remove((Grid)mainWindow.FindName("GrayedGrid"));
            mainWindow.UnregisterName("GrayedGrid");
            mainWindow.OpenPage(LastOpenedPage);
        }

        private void GenerateAnimation(ref Grid LoginWindowGrid, ref Storyboard storyboard, double from, double to)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = from;
            doubleAnimation.To = to;
            doubleAnimation.BeginTime = new TimeSpan(0, 0, 0);
            doubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(430));

            Storyboard.SetTargetName(doubleAnimation, "Transform");
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(TranslateTransform.XProperty));

            storyboard.Children.Clear();
            storyboard.Children.Add(doubleAnimation);
        }
    }
}
