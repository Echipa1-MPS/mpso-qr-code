using QR_Presence.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QR_Presence.Views.AdminPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminTabbedPage : TabbedPage
    {

        public ListOfStudentsViewModel ListOfStudent{ get; set; } = new ListOfStudentsViewModel();
        public ListOfProfessorViewModel ListOfProfessor { get; set; } = new ListOfProfessorViewModel(); 
        public ListOfCoursesViewModel ListOfCourses { get; set; } = new ListOfCoursesViewModel();

        public AdminTabbedPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}