using QR_Presence.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QR_Presence.Views.MainTabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public List<Models.TeamMembersModel> TeamMembers { get; set; } = new List<Models.TeamMembersModel>
        {
            new Models.TeamMembersModel
            {
                Name = "Stefan Marian Nedelcu",
                ProfileImage = "stefan_n.png",
                Group = "344C5",
                GitUrl = "https://github.com/StefanNedelcu",
                GitHubUser= "StefanNedelcu",
                Role ="Project Manager"
            },
            new Models.TeamMembersModel
            {
                Name = "Marina Carmina Cretu",
                ProfileImage = "carmina_cr.png",
                GitUrl="https://github.com/mariacarmina",
                GitHubUser= "mariacarmina",

                Group="344C2",
                Role =" Team Lead"
            },
            new Models.TeamMembersModel
            {
                Name = "Tudor Alexandru Calafeteanu",
                ProfileImage = "tudor_c.png",
                GitUrl="https://github.com/tcalaf",
                GitHubUser= "tcalaf",
                Group="344C5",
                Role =" Tester"
            },
            new Models.TeamMembersModel
            {
                Name = "Stefan Andrei Popa",
                ProfileImage = "stefan_p.png",
                Group = "344C5",
                GitUrl = "https://github.com/stefanp0pa",
                GitHubUser= "stefanp0pa",

                Role ="Dev"
            },
            new Models.TeamMembersModel
            {
                Name = "Stela Josan Gulica",
                ProfileImage = "profilef.png",
                GitUrl="https://github.com/stelajg",
                GitHubUser= "stelajg",

                Group="341C2",
                Role =" Dev"
            },
            new Models.TeamMembersModel
            {
                Name = "Sandu Ilie-Cristian",
                ProfileImage = "cristi_s.png",
                GitUrl="https://github.com/CristiSandu",
                GitHubUser= "CristiSandu",

                Group="344C5",
                Role =" Dev"
            }
        };
        private Button CheckButton { get; set; }

        private string Role { get; set; }
        public SettingsPage()
        {
            InitializeComponent();

            switch (Settings.Theme)
            {
                case 0:
                    defaultTheme.Style = (Style)Application.Current.Resources["ButtonUnCheckStyle"];
                    CheckButton = defaultTheme;
                    break;
                case 1:
                    lightTheme.Style = (Style)Application.Current.Resources["ButtonUnCheckStyle"];
                    CheckButton = lightTheme;
                    break;
                case 2:
                    darkTheme.Style = (Style)Application.Current.Resources["ButtonUnCheckStyle"];
                    CheckButton = darkTheme;
                    break;
                default:
                    break;
            }
            Role = Preferences.Get("Role", "2");


            BindingContext = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Role == "0")
            {
                backButton.IsVisible = true;
            }
        }

        private void DefaultTheme_Clicked(object sender, EventArgs e)
        {
            if (!(sender is Button btn))
                return;

            if (CheckButton.Text == btn.Text)
                return;

            CheckButton.Style = (Style)Application.Current.Resources["ButtonCheckedStyle"];
            switch (btn.Text)
            {
                case "Default":
                    defaultTheme.Style = (Style)Application.Current.Resources["ButtonUnCheckStyle"];
                    Settings.Theme = 0;
                    CheckButton = defaultTheme;
                    break;
                case "Light":
                    lightTheme.Style = (Style)Application.Current.Resources["ButtonUnCheckStyle"];
                    Settings.Theme = 1;
                    CheckButton = lightTheme;
                    break;
                case "Dark":
                    darkTheme.Style = (Style)Application.Current.Resources["ButtonUnCheckStyle"];
                    Settings.Theme = 2;
                    CheckButton = darkTheme;
                    break;
                default:
                    break;
            }

            Settings.SetTheme();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Warning !", "Would you like to logout?", "Yes", "No");
            if (!answer)
                return;

            SecureStorage.RemoveAll();
            if (Preferences.ContainsKey("Role"))
            {
                Preferences.Remove("Role");
            }
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        private async void backButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}