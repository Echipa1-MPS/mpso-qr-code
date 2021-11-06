using QR_Presence.Models;
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
    public partial class OverViewPage : ContentPage
    {
        public CourseInfoModel Course { get; set; } = new CourseInfoModel
        {
            Id_Cours = 1,
            Name_C = "IOCLA",
            Professor = "Prof. Razvan Deaconescu",
            Desc = "Programare in limbaj de asamblare este un curs de din Anul 2 in care se invata notiuni de hardware",
            Grading = "30% - 3 Teme \n20% Teste de curs \n50% examenul \nCerinte minime min 50% parcurs si min 50% examen \n",
            Intervals = new List<IntervalModel>
            {
                new IntervalModel
                {
                    Name="MPS",
                    Day="Marti",
                    StartHour=18,
                    Step=2
                },
                new IntervalModel
                {
                    Name="EP",
                    Day="Miercuri",
                    StartHour=12,
                    Step=2
                }
            }
        };
        public OverViewPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}