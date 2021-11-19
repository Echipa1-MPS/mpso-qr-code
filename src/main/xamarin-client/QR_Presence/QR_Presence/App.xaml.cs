using QR_Presence.Helpers;
using QR_Presence.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QR_Presence
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Settings.SetTheme();
            string isLogIn = Preferences.Get("IsLogIn", "");
            string role = Preferences.Get("Role", "");


            if (string.IsNullOrEmpty(isLogIn))
                MainPage = new NavigationPage(new LoginPage());
            else if (role == "0")
                MainPage = new NavigationPage(new Views.AdminPages.AdminTabbedPage());
            else
                MainPage = new NavigationPage(new Views.MainTabs.MainTabbedPage());
        }

        protected override void OnStart()
        {
            OnResume();
        }

        protected override void OnSleep()
        {
            Settings.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            Settings.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Settings.SetTheme();
            });
        }
    }
}
