using QR_Presence.Models;
using QR_Presence.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<CoursesEnrolled> CourseList { get; set; }
        public string Count { get; set; }

        public UserCourses Courses { get; set; }
        public CoursesPage()
        {
            InitializeComponent();
            Task.Run(async () =>
            {
                Courses = await Services.APICalls.GetUserCoursesAsync();
                CourseList = new ObservableCollection<CoursesEnrolled>(Courses.courses_enrolled);
                Count = $"Number of courses enroled: {Courses.count}";
            }).Wait();
            BindingContext = this;
        }
        private async void AccountsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            CoursesEnrolled course = e.CurrentSelection.FirstOrDefault() as CoursesEnrolled;

            if (e.CurrentSelection != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CoursePage(course));
            }

            ((CollectionView)sender).SelectedItem = null;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            int i = 0;
        }
    }
}