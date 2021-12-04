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

        public ObservableCollection<User> Professors { get; set; }
        public User Professor { get; set; }

        public List<Student> Students_Selected { get; set; }
        public int SelectedItemsNumber { get; set; }
        public Cours Course { get; set; }
        public bool IsUpdate { get; set; }

        //public ObservableCollection

        public List<DayOfWeek> DayOfWeekCourse { get; set; } = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
        public List<int> LenghtOfCourse { get; set; } = new List<int> { 1, 2, 3, 4 };

        public List<string> StartHOfCourse { get; set; } = new List<string> {
            "08:00",
            "09:00",
            "10:00",
            "11:00",
            "12:00",
            "13:00",
            "14:00",
            "15:00",
            "16:00",
            "17:00",
            "18:00"
        };

        public ObservableCollection<IntervalPicker> ListOfIntervals { get; set; } = new ObservableCollection<IntervalPicker>();



        public EditCoursePage()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                StudentsAdmin stud = await Services.APICalls.GetStudentsAdminAsync();
                TeachersAdmin prof = await Services.APICalls.GetProfessorsAdminAsync();


                ListOf = new ObservableCollection<User>(stud.students);
                Professors = new ObservableCollection<User>(prof.teachers);
            }).Wait();

            ListOfIntervals  = new ObservableCollection<IntervalPicker> { new IntervalPicker { TextButton = "plus" } };

            Course = new Cours();
            IsUpdate = false;
            BindingContext = this;
        }

        public EditCoursePage(Cours course)
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                StudentsAdmin stud = await Services.APICalls.GetStudentsAdminAsync();
                TeachersAdmin prof = await Services.APICalls.GetProfessorsAdminAsync();

                int i = 0;
                foreach (var item in course.intervals)
                {
                    string buttonTxt = i == 0 ? "plus" : "minus";
                    i++;
                    ListOfIntervals.Add(new IntervalPicker
                    {
                        Duration = item.length,
                        StartH = item.STime,
                        Day = item.day,
                        TextButton = buttonTxt,
                    });
                }

                
                ListOf = new ObservableCollection<User>(stud.students);
                Professors = new ObservableCollection<User>(prof.teachers);

                Professor = prof.teachers.Find(x => x.User_id == course.Id_Professor);
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
                Students_Selected.Add(new Student { id_user = user.User_id });
            }
            selectedNumber.Text = $"Selected Students: {Students_Selected.Count}";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            await Navigation.PopAsync();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            bool isok1 = false;
            bool isok2 = false;
            bool isok3 = false;


            if (IsUpdate)
                isok1 = await Services.APICalls.UpdateCourseAdminAsync(Course, Professor.User_id);
            else
            {
                Course.Id_Course = await Services.APICalls.CreateCourseAdminAsync(Course, Professor.User_id);
                if (Course.Id_Course != -1)
                {
                    isok1 = true;
                }
            }

            if (Students_Selected?.Count != 0)
                isok2 = await Services.APICalls.EnroleStudentsAdminAsync(new EnrolleStudents { id_course = Course.Id_Course, students_to_enroll = Students_Selected });
            if (Students_Selected == null)
            {
                isok2 = true;
            }


            if (ListOfIntervals[0] != new IntervalPicker { TextButton = "plus" })
            {
                foreach (IntervalPicker pick in ListOfIntervals)
                {
                    isok3 = await Services.APICalls.AddIntervalsCoursAsync(pick, Course.Id_Course);
                }
            }

            if (isok1 && isok2 && isok3)
                await Navigation.PopAsync();
            else
                await DisplayAlert("Alert!", "Error Ocured, retry", "OK");
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn != null)
            {
                if (btn.Text == "plus" && ListOfIntervals.Count < 3)
                {
                    ListOfIntervals.Add(new IntervalPicker { TextButton = "minus" });
                }
                else if (btn.Text == "minus")
                {
                    ListOfIntervals.RemoveAt(ListOfIntervals.Count - 1);
                }
            }
        }
    }
}