using QR_Presence.Models;
using QR_Presence.Models.APIModels;
using QR_Presence.Views;
using QR_Presence.Views.EditPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QR_Presence.ViewModels
{
    public class ListOfProfessorViewModel : BaseViewModel
    {
        public ObservableCollection<User> ListOf { get; set; }
        public string PageTitle { get; set; }
        public Command<User> Delete { get; private set; }

        User selectedElement;
        public User SelectedElement
        {
            get
            {
                return selectedElement;
            }
            set
            {
                if (selectedElement != value)
                {
                    selectedElement = value;
                    GoToNextPage.Execute(selectedElement);
                }
            }
        }
        public Command GoToNextPage { get; set; }
        public Command GoNewElementPage { get; set; }

        public ListOfProfessorViewModel()
        {

            Task.Run(async () =>
            {
                TeachersAdmin stud = await Services.APICalls.GetProfessorsAdminAsync();
                ListOf = new ObservableCollection<User>(stud.teachers);
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

            GoToNextPage = new Command(async () =>
            {
                if (SelectedElement != null)
                    await Application.Current.MainPage.Navigation.PushAsync(new EditProfile{ BindingContext = SelectedElement });
            });

            GoNewElementPage = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage(false));
            });

            PageTitle = $"Count {ListOf.Count}";

        }
    }
}
