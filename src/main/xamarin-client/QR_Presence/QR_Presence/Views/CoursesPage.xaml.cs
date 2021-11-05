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

        public List<Models.CourseModel> CourseList { get; set; } = new List<Models.CourseModel>
        {
            new Models.CourseModel
            {
                Name="MPS",
                Prof="vasile",
                Desc="",
                Gradings=""
            },
            new Models.CourseModel
            {
                Name="EP",
                Prof="Vali",
                Desc="",
                Gradings=""
            },
            new Models.CourseModel
            {
                Name="SADAS",
                Prof="Valirwerwe",
                Desc="",
                Gradings=""
            }
        };
        public CoursesPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}