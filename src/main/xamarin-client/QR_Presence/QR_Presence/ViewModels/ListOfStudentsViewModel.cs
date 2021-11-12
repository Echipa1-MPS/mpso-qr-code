using QR_Presence.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace QR_Presence.ViewModels
{
    public class ListOfStudentsViewModel : BaseViewModel
    {
        public ObservableCollection<UserModel> ListOf { get; set; }
        public string PageTitle { get; set; }
        public Command<UserModel> Delete { get; private set; }


        public ListOfStudentsViewModel()
        {
            ListOf = new ObservableCollection<UserModel>
            {
                new UserModel
                {
                    Name = "Mihai",
                    SecondName = "Vasile",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai",
                    SecondName = "Vasile",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai",
                    SecondName = "Vasile",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai",
                    SecondName = "Vasile",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai",
                    SecondName = "Vasile",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                }

            };
            Delete = new Command<UserModel>(async model =>
            {
                ListOf.Remove(model);
                PageTitle = $"Count {ListOf.Count}";
                OnPropertyChanged(nameof(PageTitle));

            });
            PageTitle = $"Count {ListOf.Count}";

        }
    }
}
