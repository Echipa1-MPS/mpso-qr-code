using QR_Presence.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QR_Presence.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public List<Models.IntervalModel> NextCourses { get; set; } = new List<Models.IntervalModel>
        {
            new Models.IntervalModel
            {
                Name="MPS",
                Day="Marti",
                StartHour=18,
                Step=2
            },
            new Models.IntervalModel
            {
                Name="EP",
                Day="Miercuri",
                StartHour=12,
                Step=2
            },
            new Models.IntervalModel
            {
                Name="UBD",
                Day="Joi",
                StartHour=8,
                Step=2
            },
            new Models.IntervalModel
            {
                Name="UBD",
                Day="Joi",
                StartHour=8,
                Step=2
            },
            new Models.IntervalModel
            {
                Name="UBD",
                Day="Joi",
                StartHour=8,
                Step=2
            }
        };

        public User User { get; set; }

        public ProfilePage()
        {
            InitializeComponent();
            Task.Run(async () =>
            {
                User = await Services.DatabaseConnection.GetUser();
            }).Wait();
            BindingContext = this;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPages.EditProfile
            {
                BindingContext = User
            });
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScanPage());
        }
    }
}