using QR_Presence.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QR_Presence.Views.EditPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfile : ContentPage
    {

        public bool IsVisibleOnStudent { get; set; }
        public bool IsVisibleOnAdmin { get; set; }
        public string Password { get;set; }

        public User Profile { get; set; }
        public EditProfile()
        {
            InitializeComponent();

        }


        public EditProfile(User user)
        {
            InitializeComponent();

            string role = Preferences.Get("Role", "2");

            if (role == "0")
            {
                IsVisibleOnStudent = false;
                IsVisibleOnAdmin = true;
            }
            else
            {
                IsVisibleOnStudent = true;
                IsVisibleOnAdmin = false;
            }
            Profile= user;
            BindingContext = this;
        }

        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {

            string role = Preferences.Get("Role", "2");
            bool IsUpdated = false;
            if (role == "0")
            {
                IsUpdated = await Services.APICalls.UpdateUserAdminAsync((BindingContext as User));
            }
            else
            {
                IsUpdated = await Services.APICalls.UpdateUserStudentAsync(Profile, Password);
            }

            if (IsUpdated)
            {
                await DisplayAlert("All Ok", "Login succesfully", "OK");
                SecureStorage.RemoveAll();
                if (Preferences.ContainsKey("Role"))
                {
                    Preferences.Remove("Role");
                    Preferences.Remove("IsLogIn");

                }
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                await DisplayAlert("Alert!", "Error Ocured, retry", "OK");
                return;
            }

            await Navigation.PopAsync();
        }
    }
}