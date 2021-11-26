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

namespace QR_Presence.Views.AdminPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCoursePage : ContentPage
    {
        public ObservableCollection<User> ListOf { get; set; } 
        public List<Student> Students_Selected { get; set; }
        public int SelectedItemsNumber { get; set; }
        public CourseInfoModel Course { get; set; }
        public bool IsUpdate { get; set; }

        public EditCoursePage()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                StudentsAdmin stud = await Services.APICalls.GetStudentsAdminAsync();
                ListOf = new ObservableCollection<User>(stud.students);
            }).Wait();

            Course = new CourseInfoModel();
            IsUpdate = false;
            BindingContext = this;
        }

        public EditCoursePage(CourseInfoModel course)
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                StudentsAdmin stud = await Services.APICalls.GetStudentsAdminAsync();
                ListOf = new ObservableCollection<User>(stud.students);
            }).Wait();

            Course = course;
            IsUpdate = true;
            BindingContext = this;
        }

        private void accountsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Students_Selected = new List<Student>();
            for (int i = 0; i < e.CurrentSelection.Count; i++)
            {
                User user = e.CurrentSelection[i] as User;
                Students_Selected.Add(new Student { id_user = user.user_id });
            }
            selectedNumber.Text = $"Selected Students: {Students_Selected.Count}";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            bool isok1;
            bool isok2 = false;

            if (IsUpdate)
                isok1 = await Services.APICalls.UpdateCourseAdminAsync(Course, 2);
            else
                isok1 = await Services.APICalls.CreateCourse(Course, 2);

            if (Students_Selected.Count != 0)
                isok2 = await Services.APICalls.EnroleStudentsAdminAsync(new EnrolleStudents { id_course = Course.Id_Course, students_to_enroll = Students_Selected});
            
            if (isok1 && isok2)
                await Navigation.PopAsync();
            else
                await DisplayAlert("Alert!", "Error Ocured, retry", "OK");
        }
    }
}