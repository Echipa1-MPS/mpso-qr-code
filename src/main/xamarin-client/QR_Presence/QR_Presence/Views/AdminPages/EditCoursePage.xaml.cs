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
                    Id_User=1,
                    Name = "Mihai",
                    SecondName = "Vasile",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Id_User=2,

                    Name = "Mihai",
                    SecondName = "Vasile",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Id_User=3,
                    Name = "Mihai",
                    SecondName = "Vasile",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP = "mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Id_User=4,
                    Name = "Mihai",
                    SecondName = "Vasile",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP = "mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Id_User=5,
                    Name = "Mihai",
                    SecondName = "Vasile",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP = "mihai_vasile",
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
                Students_Selected.Add(user.Id_User);
            }
            selectedNumber.Text = $"Selected Students: {Students_Selected.Count.ToString()}";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}