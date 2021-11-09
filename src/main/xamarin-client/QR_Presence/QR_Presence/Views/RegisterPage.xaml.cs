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
    public partial class RegisterPage : ContentPage
    {
        public List<string> Roles { get; set; } = new List<string> {
            "Admin",
            "Student",
            "Professor"
        };
        
        public string SelectedRole { get; set; }


        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfPassword { get; set; }
        public string Group { get; set; }


        public UserModel User { get; set; } 

        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            string[] words = Email.Split('@');

            if (!Password.Equals(ConfPassword))
            {
                await DisplayAlert("Alert!", "Conf Pass Incorect", "OK");
                return;
            }
            
            if (!IsValidEmail(Email) || !words[1].Equals("stud.acs.upb.ro") )
            {
                await DisplayAlert("Alert!", "Incomplet Email address", "OK");
                return;
            }

            int role = 2;
            switch (SelectedRole)
            {
                case "Admin":
                    role = 0;
                    break;
                case "Professor":
                    role = 1;
                    break;
                case "Student":
                    role = 2;
                    break;
                default:
                    break;
            }


            User = new UserModel
            {
                Name = Name,
                SecondName = SecondName,
                LDAP = words[0],
                Email = Email,
                Group = Group,
                Privilege = role,
            };

        }

        private bool IsValidEmail(string email)
        {
            try
            {
                System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
           await Navigation.PopAsync();
        }
    }
}