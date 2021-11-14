using Newtonsoft.Json;
using QR_Presence.Helpers;
using QR_Presence.Models;
using QR_Presence.Models.APIModels;
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
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            using (var c = new HttpClient())
            {
                var client = new HttpClient();
                var jsonRequest = new
                {
                    email = Email,
                    password = Password,
                };

                var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
                HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(new Uri("http://ec2-3-18-103-144.us-east-2.compute.amazonaws.com:8080/api/user/login"), content);

                if (response.IsSuccessStatusCode)
                {
                    UserModel user = await Services.DatabaseConnection.GetUser();
                    LoginResponse result = JsonConvert.DeserializeObject<LoginResponse>(response.Content.ReadAsStringAsync().Result);


                    user.Id_User = Int32.Parse(result.user_id);

                    await Services.DatabaseConnection.UpdateUser(user);
                    await DisplayAlert("All Ok", "Login succesfully", "OK");
                }
                else
                {
                    await DisplayAlert("Alert!", "Error Ocured, retry", "OK");
                }
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
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}