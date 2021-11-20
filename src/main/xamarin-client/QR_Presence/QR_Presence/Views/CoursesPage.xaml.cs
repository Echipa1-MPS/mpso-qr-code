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
    public partial class CoursesPage : ContentPage
    {
        public List<CourseInfoModel> CourseList { get; set; } = new List<Models.CourseInfoModel>
        {
            new Models.CourseInfoModel
            {
                Id_Course = 3,
                Name_C="MPS",
                Professor="vasile",
                Desc="",
                Grading="",
                Intervals = new List<Models.IntervalModel>{
                    new Models.IntervalModel
                    {
                        Name="MPS",
                        Day="Marti",
                        StartHour=18,
                        Step=2
                    },

                    new Models.IntervalModel
                    {
                        Name="MPS",
                        Day="Miercuri",
                        StartHour=12,
                        Step=2
                    }
                }
            },
            new Models.CourseInfoModel
            {
                Id_Course = 2,
                Name_C="EP",
                Professor="vasile",
                Desc="",
                Grading="",
                Intervals = new List<Models.IntervalModel>{
                    new Models.IntervalModel
                    {
                        Name="EP",
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
                    }
                }
            },
            new Models.CourseInfoModel
            {
                Id_Course = 1,
                Name_C = "IOCLA",
                Professor = "Prof. Razvan Deaconescu",
                Desc = "Programare in limbaj de asamblare este un curs de din Anul 2 in care se invata notiuni de hardware",
                Grading = "30% 3 Teme \n20% Teste de curs \n50% examenul \nCerinte minime min 50% parcurs si min 50% examen \n",
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
            }
        };
        public CoursesPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public List<CourseInfoModel> Courses_Selected { get; set; } = new List<CourseInfoModel>();
        public List<int> Courses_ID_List { get; set; } = new List<int>();

        private async void AccountsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.CurrentSelection.Count == 0)
                return;
            
            CourseInfoModel course = e.CurrentSelection.FirstOrDefault() as CourseInfoModel;

            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync(new CoursePage(course));
            }
            ((CollectionView)sender).SelectedItem = null;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            int i = 0;
        }
    }
}