using QR_Presence.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QR_Presence.Views.MainTabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private Button CheckButton { get; set; }
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
    }
}