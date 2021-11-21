using Newtonsoft.Json;
using QR_Presence.Helpers;
using QR_Presence.Models;
using QR_Presence.Models.APIModels;
using QR_Presence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QR_Presence.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public string Password { get; set; }
        public string Email { get; set; }

        public bool IsLoading{ get; set; } = false;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            loading.IsRunning = true;
            if (!await APICalls.LoginUser(Email, Password))
            {
                await DisplayAlert("Alert!", "Error Ocured, retry", "OK");
                loading.IsRunning = false;
                return;
            }

            string role = Preferences.Get("Role", "2");

            switch (role)
            {
                case "0":
                    await Navigation.PushAsync(new AdminPages.AdminTabbedPage());
                    break;
                default:
                    await Navigation.PushAsync(new MainTabs.MainTabbedPage());
                    break;
            }
            loading.IsRunning = false;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}