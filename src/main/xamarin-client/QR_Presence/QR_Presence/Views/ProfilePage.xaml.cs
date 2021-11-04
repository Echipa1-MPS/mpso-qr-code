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
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


        }
    }
}