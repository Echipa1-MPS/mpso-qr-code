using QR_Presence.Models;
using QR_Presence.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QR_Presence.ViewModels
{
    public class ListOfStudentsViewModel : BaseViewModel
    {
        public ObservableCollection<User> ListOf { get; set; }
        public string PageTitle { get; set; }
        public Command<User> Delete { get; private set; }
        public ListOfStudentsViewModel()
        {
            Task.Run(async () =>
            {
                StudentsAdmin stud = await Services.APICalls.GetStudentsAdminAsync();
                ListOf = new ObservableCollection<User>(stud.students);
            }).Wait();

            Delete = new Command<User>(async model =>
            {
                if (await Services.APICalls.DeleteUserAdminAsync(model.email))
                {
                    ListOf.Remove(model);
                    PageTitle = $"Count {ListOf.Count}";
                    OnPropertyChanged(nameof(PageTitle));
                }
            });

            PageTitle = $"Count {ListOf.Count}";
        }
    }
}
