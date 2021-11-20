using Newtonsoft.Json;
using QR_Presence.Helpers;
using QR_Presence.Models;
using QR_Presence.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QR_Presence.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public List<string> Roles { get; set; } = new List<string> {
            "Admin",
            "Student",
            "Professor"
        };

        public List<string> ProffesorDomain { get; set; } = new List<string> {
            "upb.ro",
            "onmicrosoft.upb.ro",
            "cs.pub.ro",
            "cti.upb.ro"
        };
        public string SelectedRole { get; set; }

        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } = "";
        public string ConfPassword { get; set; } = "";
        public string Group { get; set; }


        public User User { get; set; }

        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public bool Verify { get; set; } = true;

        public RegisterPage(bool role)
        {
            InitializeComponent();
            Verify = role;
            BindingContext = this;
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            string[] words = Email.Split('@');

            if (!Password.Equals(ConfPassword) && Verify)
            {
                await DisplayAlert("Alert!", "Conf Pass Incorect", "OK");
                return;
            }

            if (!IsValidEmail(Email, SelectedRole, words[1]))
            {
                await DisplayAlert("Alert!", "Incomplet Email address", "OK");
                return;
            }

            RoleEnum role = RoleEnum.Student;
            switch (SelectedRole)
            {
                case "Admin":
                    role = RoleEnum.Admin;
                    break;
                case "Professor":
                    role = RoleEnum.Professor;
                    break;
                case "Student":
                    role = RoleEnum.Student;
                    break;
                default:
                    break;
            }

            User = new User
            {
                name = Name,
                surname = SecondName,
                username = words[0],
                email =  Email ,
                group = Group ,
                Privilege = (int)role,
            };

            bool respons_add = Verify ? await Services.APICalls.RegisterUser(User, Password) : await Services.APICalls.AddUserAdminAsync(User, $"{words[0]}/{Group}");

            if (respons_add)
            {
                await DisplayAlert("All Ok", "Account Registered", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Alert!", "Error Ocured, retry", "OK");
            }
        }

        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private bool IsValidEmail(string email, string selectedRole, string LDAP)
        {
            try
            {
                System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(email);
                if (!(addr.Address == email))
                {
                    return false;
                }
                else
                {
                    switch (selectedRole)
                    {
                        case "Admin":
                            return ProffesorDomain.Any(s => LDAP.Contains(s));
                        case "Professor":
                            return ProffesorDomain.Any(s => LDAP.Contains(s));
                        case "Student":
                            return LDAP == "stud.acs.upb.ro";
                        default:
                            return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}