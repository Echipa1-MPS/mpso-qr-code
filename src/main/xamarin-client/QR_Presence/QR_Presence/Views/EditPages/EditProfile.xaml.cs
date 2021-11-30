using QR_Presence.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QR_Presence.Views.EditPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfile : ContentPage
    {
        public EditProfile()
        {
            InitializeComponent();
        }

        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {

            if (await Services.APICalls.UpdateUserAdminAsync((BindingContext as User)))
            {
                await DisplayAlert("All Ok", "Login succesfully", "OK");
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