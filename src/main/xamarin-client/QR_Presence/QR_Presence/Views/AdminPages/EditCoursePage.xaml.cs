using QR_Presence.Models;
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
        public ObservableCollection<UserModel> ListOf { get; set; } = new ObservableCollection<UserModel>
            {
                new UserModel
                {
                    user_id=1,
                    name = "Mihai",
                    surname = "Vasile",
                    email = "mihai_vasile@stud.acs.upb.ro",
                    group = "344CC",
                    username ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    user_id=2,

                    name = "Mihai",
                    surname = "Vasile",
                    email = "mihai_vasile@stud.acs.upb.ro",
                    group = "344CC",
                    username ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    user_id=3,
                    name = "Mihai",
                    surname = "Vasile",
                    email = "mihai_vasile@stud.acs.upb.ro",
                    group = "344CC",
                    username = "mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    user_id=4,
                    name = "Mihai",
                    surname = "Vasile",
                    email = "mihai_vasile@stud.acs.upb.ro",
                    group = "344CC",
                    username = "mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    user_id=5,
                    name = "Mihai",
                    surname = "Vasile",
                    email = "mihai_vasile@stud.acs.upb.ro",
                    group = "344CC",
                    username = "mihai_vasile",
                    Privilege = 2
                }

            };

        public List<int> Students_Selected { get; set; }
        public int SelectedItemsNumber { get; set; }
        public CourseInfoModel Course { get; set; }

        public EditCoursePage()
        {
            InitializeComponent();
            Course = new CourseInfoModel();
            BindingContext = this;
        }
        public EditCoursePage(CourseInfoModel course)
        {
            InitializeComponent();
            Course = course;
            BindingContext = this;
        }

        private void accountsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Students_Selected = new List<int>();
            for (int i = 0; i < e.CurrentSelection.Count; i++)
            {
                UserModel user = e.CurrentSelection[i] as UserModel;
                Students_Selected.Add(user.user_id);
            }
            selectedNumber.Text = $"Selected Students: {Students_Selected.Count.ToString()}";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}