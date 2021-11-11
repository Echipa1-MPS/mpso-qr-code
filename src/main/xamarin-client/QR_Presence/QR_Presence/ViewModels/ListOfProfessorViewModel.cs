using QR_Presence.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QR_Presence.ViewModels
{
    public class ListOfProfessorViewModel : BaseViewModel
    {
        public ObservableCollection<UserModel> ListOf { get; set; }
        public string PageTitle { get; set; }
        public Command<UserModel> Delete { get; private set; }

        public ListOfProfessorViewModel()
        {
            ListOf = new ObservableCollection<UserModel>
            {
                new UserModel
                {
                    Name = "Mihai",
                    SecondName = "Vasile",
                    Email = "mihai_vasile1@upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai1",
                    SecondName = "Vasile2",
                    Email = "mihai_vasile2@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai4",
                    SecondName = "Vasile3",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai4",
                    SecondName = "Vasile32",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai23",
                    SecondName = "Vasile23423",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai4",
                    SecondName = "Vasile32",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai23",
                    SecondName = "Vasile23423",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai4",
                    SecondName = "Vasile32",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai23",
                    SecondName = "Vasile23423",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai4",
                    SecondName = "Vasile32",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai23",
                    SecondName = "Vasile23423",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai4",
                    SecondName = "Vasile32",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai23",
                    SecondName = "Vasile23423",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai4",
                    SecondName = "Vasile32",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai23",
                    SecondName = "Vasile23423",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai4",
                    SecondName = "Vasile32",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },
                new UserModel
                {
                    Name = "Mihai23",
                    SecondName = "Vasile23423",
                    Email = "mihai_vasile@stud.acs.upb.ro",
                    Group = "344CC",
                    LDAP ="mihai_vasile",
                    Privilege = 2
                },

            };
            Delete = new Command<UserModel>(model =>
            {
                ListOf.Remove(model);
                PageTitle = $"Count {ListOf.Count}";
                OnPropertyChanged(nameof(PageTitle));
            });
            PageTitle = $"Count {ListOf.Count}";

        }
    }
}
